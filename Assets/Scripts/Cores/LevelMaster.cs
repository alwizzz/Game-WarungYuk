using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelMaster : MonoBehaviour
{
    [Header("Level State")]
    [SerializeField] bool isPaused;
    [SerializeField] bool gameHasStarted;

    [Header("Cooking Recipe")]
    [SerializeField] TextAsset jsonFile;
    CookingRecipe cookingRecipe;
    [SerializeField] List<ProcessedIngredient> processedIngredientPrefabs;
    [SerializeField] List<CompletedDish> completedDishPrefabs;
    [SerializeField] List<int> completedDishSpawnCounters;
    bool stillHasCounters;

    [Header("Point Config")]
    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] int totalPoints;
    [SerializeField] int oneStarMinimumPoints;
    [SerializeField] int twoStarMinimumPoints;
    [SerializeField] int threeStarMinimumPoints;
    [SerializeField] float wrongDishPenaltyMultiplier;
    [SerializeField] float customerIsAngryPenaltyMultiplier;

    [Header("Level Statistic")]
    [SerializeField] int successfulOrders;
    [SerializeField] int failedOrders;

    [Header("Spawn Config")]
    [SerializeField] float initialSpawnDelayMin;
    [SerializeField] float initialSpawnDelayMax;
    [SerializeField] float spawnDelayMin;
    [SerializeField] float spawnDelayMax;
    
    [Header("Duration Config")]
    [SerializeField] LevelTimer levelTimer;
    [SerializeField] float levelDuration;
    [SerializeField] float toBeAngryDuration;

    [Header("UI Cache")]
    [SerializeField] GameObject modalPause;
    [SerializeField] GameObject modalCookingGuide;


    private void Awake()
    {
        cookingRecipe = JsonUtility.FromJson<CookingRecipe>(jsonFile.ToString());
        gameHasStarted = false;
        levelTimer.SetLevelDuration(levelDuration);
        isPaused = false;
        modalPause.SetActive(false);
    }

    private void Start()
    {
        UpdateStillHasCounters();
        UpdateDisplayPoint();

        successfulOrders = 0;
        failedOrders = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //isPaused = !isPaused;
            if (!isPaused)
            {
                Pause();
            } 
            else
            {
                Unpause();
            }
        }

    }
    public bool IsPaused() => isPaused;

    public CookingRecipe GetCookingRecipe() => cookingRecipe;

    public CompletedDish GetCompletedDishPrefab(string codeName)
    {
        var cDish = completedDishPrefabs.Find((cd) => cd.GetCodeName() == codeName);
        return cDish ? cDish : null;
    }
    public ProcessedIngredient GetProcessedIngredientPrefab(string codeName)
    {
        var pIng = processedIngredientPrefabs.Find((pIng) => pIng.GetCodeName() == codeName);
        return pIng ? pIng : null;
    }

    public void IncreasePoint(int dishPoint, bool customerIsAngry) 
    {
        successfulOrders++;

        if (customerIsAngry)
        {
            float temp = (customerIsAngryPenaltyMultiplier * dishPoint);
            temp /= 100f;
            int penaltiedPoint = Mathf.RoundToInt(temp) * 100;

            totalPoints += penaltiedPoint;
            Debug.Log("Increased point by " + penaltiedPoint.ToString("n0"));
        }
        else
        {
            totalPoints += dishPoint;
            Debug.Log("Increased point by " + dishPoint.ToString("n0"));
        }

        UpdateDisplayPoint();
    }
    public void DecreasePoint(int dishPoint)
    {
        failedOrders++;

        float temp = (wrongDishPenaltyMultiplier * dishPoint);
        temp /= 100f;
        int penaltiedPoint = Mathf.RoundToInt(temp) * 100;

        totalPoints -= penaltiedPoint;
        totalPoints = (totalPoints >= 0 ? totalPoints : 0);

        Debug.Log("Decreased point by " + penaltiedPoint.ToString("n0"));
        UpdateDisplayPoint();
    }

    void UpdateDisplayPoint()
    {
        pointText.text = "Rp" + totalPoints.ToString();
    }

    public List<CompletedDish> GetCompletedDishPrefabs() => completedDishPrefabs;
    public float GetInitialSpawnDelayMin() => initialSpawnDelayMin;
    public float GetInitialSpawnDelayMax() => initialSpawnDelayMax;
    public float GetSpawnDelayMin() => spawnDelayMin;
    public float GetSpawnDelayMax() => spawnDelayMax;
    public float GetToBeAngryDuration() => toBeAngryDuration;

    void UpdateStillHasCounters()
    {
        bool value = false;
        foreach (int x in completedDishSpawnCounters)
        {
            if(x > 0) {
                value = true;    
                break; 
            }
        }
        stillHasCounters = value;
    }

    public CompletedDish GetRandomCompletedDishPrefab()
    {
        int randomIndex = Random.Range(0, completedDishPrefabs.Count);
        if (stillHasCounters)
        {
            while(completedDishSpawnCounters[randomIndex] == 0)
            {
                randomIndex = Random.Range(0, completedDishPrefabs.Count);
            } 

            completedDishSpawnCounters[randomIndex] -= 1;
            UpdateStillHasCounters();
        }

        return completedDishPrefabs[randomIndex];
    }

    public bool GameHasStarted() => gameHasStarted;
    public void StartLevel()
    {
        gameHasStarted = true;
        levelTimer.ContinueTimer();
        Debug.Log("LEVEL TIMER: GAME STARTED");
    }

    public void EndLevel()
    {
        Debug.Log("LEVEL TIMER: TIME'S UP");

        int obtainedStars = 0;
        if(totalPoints > oneStarMinimumPoints) { obtainedStars = 1; }
        if(totalPoints > twoStarMinimumPoints) { obtainedStars = 2; }
        if(totalPoints > threeStarMinimumPoints) { obtainedStars = 3; }

        FindObjectOfType<GameMaster>().SetLevelProgressCache(successfulOrders, failedOrders, totalPoints, obtainedStars);
        FindObjectOfType<SceneLoader>().LoadAfterLevel(cookingRecipe.region);
    }


    // ======================== UI

    public void Pause()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();

        if (!gameHasStarted) { return; }

        Time.timeScale = 0f;
        levelTimer.PauseTimer();

        modalPause.SetActive(true);
        isPaused = true;

        CloseCookingGuide();
    }

    public void Unpause()
    {

        Time.timeScale = 1f;
        levelTimer.ContinueTimer();

        modalPause.SetActive(false);
        isPaused = false;
        FindObjectOfType<AudioMaster>().PlayClickSFX();
    }

    public void BackToMap()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();

        if (isPaused) { Time.timeScale = 1f; }
        FindObjectOfType<SceneLoader>().LoadMapScene();
    }

    public void OpenCookingGuide()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();

        modalCookingGuide.SetActive(true);
    }

    public void CloseCookingGuide()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();

        modalCookingGuide.SetActive(false);
        if (!gameHasStarted) { StartLevel(); }
    }
}
