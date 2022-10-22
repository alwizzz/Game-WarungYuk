using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelHoverCard : MonoBehaviour
{
    [SerializeField] string levelName;
    [SerializeField] GameMaster.LevelData levelData;
    [SerializeField] GameObject star1;
    [SerializeField] GameObject star2;
    [SerializeField] GameObject star3;
    [SerializeField] TextMeshProUGUI highscoreText;

    private void Start()
    {
        levelData = FindObjectOfType<GameMaster>().GetLevelData(levelName);
        if (levelData.HasBeenCompleted()) { Setup(); }
    }

    void Setup()
    {
        highscoreText.text = levelData.GetHighscore().ToString();
        SetupStars();
    }

    void SetupStars()
    {
        int obtainedStar = levelData.GetObtainedStar();
        if (obtainedStar > 0)
        {
            star1.SetActive(true);
        }

        if (obtainedStar > 1)
        {
            star2.SetActive(true);
        }

        if (obtainedStar > 2)
        {
            star3.SetActive(true);
        }
    }
}
