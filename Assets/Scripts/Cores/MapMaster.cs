using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaster : MonoBehaviour
{
    [SerializeField] GameObject levelJawaHandler;
    [SerializeField] GameObject levelPapuaHandler;
    [SerializeField] GameObject levelSulawesiHandler;
    [SerializeField] GameObject levelKalimantanHandler;

    private void Start()
    {
        SetupLevels();
    }

    void SetupLevels()
    {
        var gameMaster = FindObjectOfType<GameMaster>();

        if (gameMaster.GetLevelData("Sumatra").HasBeenCompleted())
        {
            levelJawaHandler.SetActive(true);
            if (gameMaster.GetLevelData("Jawa").HasBeenCompleted())
            {
                levelPapuaHandler.SetActive(true);
                if (gameMaster.GetLevelData("Papua").HasBeenCompleted())
                {
                    levelSulawesiHandler.SetActive(true);
                    if (gameMaster.GetLevelData("Sulawesi").HasBeenCompleted())
                    {
                        levelKalimantanHandler.SetActive(true);
                    }
                }
            }
        }
    }
}
