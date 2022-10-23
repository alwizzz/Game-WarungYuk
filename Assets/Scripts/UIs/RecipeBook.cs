using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBook : MonoBehaviour
{
    [SerializeField] string levelName;
    [SerializeField] GameObject unlockedPage;
    [SerializeField] GameObject lockedPage;

    private void Start()
    {
        SetupPage();
    }

    void SetupPage()
    {
        var levelData = FindObjectOfType<GameMaster>().GetLevelData(levelName);

        if (levelData.HasBeenCompleted())
        {
            lockedPage.SetActive(false);
            unlockedPage.SetActive(true);
        }
        else
        {
            lockedPage.SetActive(true);
            unlockedPage.SetActive(false);
        }
    }
}
