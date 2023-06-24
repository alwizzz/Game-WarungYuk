using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameMaster : MonoBehaviour
{
    [Serializable]
    public class LevelData
    {
        [SerializeField] string levelName;
        [SerializeField] bool hasBeenCompleted;
        [SerializeField] int highscore;
        [SerializeField] int obtainedStar;

        public LevelData(string levelName)
        {
            this.levelName = levelName;
            hasBeenCompleted = false;
            highscore = 0;
            obtainedStar = 0;
        }

        public void Update(bool hasBeenCompleted, int highscore, int obtainedStar)
        {
            this.hasBeenCompleted = hasBeenCompleted;
            this.highscore = highscore;
            this.obtainedStar = obtainedStar;
        }

        public bool HasBeenCompleted() => hasBeenCompleted;
        public int GetHighscore() => highscore;
        public int GetObtainedStar() => obtainedStar;

    }

    [Serializable]
    public class SaveData
    {
        LevelData levelDataSumatra;
        LevelData levelDataJawa;
        LevelData levelDataPapua;
        LevelData levelDataSulawesi;
        LevelData levelDataKalimantan;

        public SaveData()
        {
            this.levelDataSumatra = new LevelData("Sumatra");
            this.levelDataJawa = new LevelData("Jawa");
            this.levelDataPapua = new LevelData("Papua");
            this.levelDataSulawesi = new LevelData("Sulawesi");
            this.levelDataKalimantan = new LevelData("Sumatra");
        }

        public SaveData(
            LevelData levelDataSumatra,
            LevelData levelDataJawa,
            LevelData levelDataPapua,
            LevelData levelDataSulawesi,
            LevelData levelDataKalimantan
        )
        {
            this.levelDataSumatra = levelDataSumatra;
            this.levelDataJawa = levelDataJawa;
            this.levelDataPapua = levelDataPapua;
            this.levelDataSulawesi = levelDataSulawesi;
            this.levelDataKalimantan = levelDataKalimantan;
        }

        public LevelData GetLevelData(string levelName)
        {
            if (levelName == "Sumatra") { return levelDataSumatra; }
            else if (levelName == "Jawa") { return levelDataJawa; }
            else if (levelName == "Papua") { return levelDataPapua; }
            else if (levelName == "Sulawesi") { return levelDataSulawesi; }
            else if (levelName == "Kalimantan") { return levelDataKalimantan; }
            else return null;
        }

    }

    [Header("Master")]
    [SerializeField] string dataName = "warungyuk.dat"; //warungyuk_data.dat
    [SerializeField] bool hasGameData;


    [Header("Level Data")]
    [SerializeField] LevelData levelDataSumatra;
    [SerializeField] LevelData levelDataJawa;
    [SerializeField] LevelData levelDataPapua;
    [SerializeField] LevelData levelDataSulawesi;
    [SerializeField] LevelData levelDataKalimantan;

    [Header("Level Progress Cache")]
    [SerializeField] int successfulOrders;
    [SerializeField] int failedOrders;
    [SerializeField] int totalPoints;
    [SerializeField] int obtainedStars;

    private void Awake()
    {
        UpdateHasGameData();
    }

    void UpdateHasGameData()
    {
        hasGameData = File.Exists(Application.persistentDataPath + "/" + dataName);
    }


    void SaveGame(bool isResetting = false)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + dataName);

        SaveData data;
        if (isResetting)
        {
            data = new SaveData();
        } else
        {
            data = new SaveData(
                levelDataSumatra,
                levelDataJawa,
                levelDataPapua,
                levelDataSulawesi,
                levelDataKalimantan
            );
        }

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved");
        UpdateHasGameData();
    }

    public void LoadGame()
    {
        if (hasGameData)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath
                                + "/" + dataName, FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            levelDataSumatra = data.GetLevelData("Sumatra");
            levelDataJawa = data.GetLevelData("Jawa");
            levelDataPapua = data.GetLevelData("Papua");
            levelDataSulawesi = data.GetLevelData("Sulawesi");
            levelDataKalimantan = data.GetLevelData("Kalimantan");

            Debug.Log("Game data loaded");
        }
        else
        {
            Debug.LogError("Game data doesn't exist");
        }
    }

    public void ResetAndLoadGame()
    {
        SaveGame(true);
        LoadGame();
    }

    public void UpdateLevelDataAndSaveGame(string levelName, bool hasBeenCompleted, int highscore, int obtainedStar)
    {
        if (levelName == "Sumatra") { levelDataSumatra.Update(hasBeenCompleted, highscore, obtainedStar); }
        else if (levelName == "Jawa") { levelDataJawa.Update(hasBeenCompleted, highscore, obtainedStar); }
        else if (levelName == "Papua") { levelDataPapua.Update(hasBeenCompleted, highscore, obtainedStar); }
        else if (levelName == "Sulawesi") { levelDataSulawesi.Update(hasBeenCompleted, highscore, obtainedStar); }
        else if (levelName == "Kalimantan") { levelDataKalimantan.Update(hasBeenCompleted, highscore, obtainedStar); }
        else { Debug.Log("invalid levelName"); }

        SaveGame();
    }

    public LevelData GetLevelData(string levelName)
    {
        if (levelName == "Sumatra") { return levelDataSumatra; }
        else if (levelName == "Jawa") { return levelDataJawa; }
        else if (levelName == "Papua") { return levelDataPapua; }
        else if (levelName == "Sulawesi") { return levelDataSulawesi; }
        else if (levelName == "Kalimantan") { return levelDataKalimantan; }
        else { return null; }
    }

    public bool HasGameData() => hasGameData;

    // ========================= Cache
    public void SetLevelProgressCache(int successfulOrders, int failedOrders, int totalPoints, int obtainedStars)
    {
        this.successfulOrders = successfulOrders;
        this.failedOrders = failedOrders;
        this.totalPoints = totalPoints;
        this.obtainedStars = obtainedStars;
    }

    public int GetAndResetSuccessfulOrders()
    {
        var temp = successfulOrders;
        successfulOrders = 0;
        return temp;
    }
    public int GetAndResetFailedOrders() 
    {
        var temp = failedOrders;
        failedOrders = 0;
        return temp;
    }
    public int GetAndResetTotalPoints()
    {
        var temp = totalPoints;
        totalPoints = 0;
        return temp;
    }
    public int GetAndResetObtainedStars()
    {
        var temp = obtainedStars;
        obtainedStars = 0;
        return temp;
    }

    public void DeleteGameData()
    {
        if (hasGameData)
        {
            File.Delete(Application.persistentDataPath
                          + "/" + dataName);
        }
    }
}
