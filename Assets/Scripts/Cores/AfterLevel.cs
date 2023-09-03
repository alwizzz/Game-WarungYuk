using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AfterLevel : MonoBehaviour
{
    [SerializeField] string levelName;

    [SerializeField] int successfulOrders;
    [SerializeField] int failedOrders;
    [SerializeField] int totalPoints;
    [SerializeField] int obtainedStars;

    [SerializeField] bool isCompleted;

    [SerializeField] GameObject successBadge;
    [SerializeField] GameObject failBadge;

    [SerializeField] GameObject star1;
    [SerializeField] GameObject star2;
    [SerializeField] GameObject star3;

    [SerializeField] TextMeshProUGUI successfulOrdersText;
    [SerializeField] TextMeshProUGUI failedOrdersText;
    [SerializeField] TextMeshProUGUI totalPointsText;

    [SerializeField] AudioClip winTrack;
    [SerializeField] AudioClip loseTrack;

    private void Start()
    {
        FetchLevelProgress();
        PlayAfterLevelTrack();

        SetupBadge();
        SetupStars();
        SetupTexts();

        if (isCompleted)
        {
            SaveLevelProgress();
        }
    }

    void FetchLevelProgress()
    {
        var gameMaster = FindObjectOfType<GameMaster>();

        this.successfulOrders = gameMaster.GetAndResetSuccessfulOrders();
        this.failedOrders = gameMaster.GetAndResetFailedOrders();
        this.totalPoints = gameMaster.GetAndResetTotalPoints();
        this.obtainedStars = gameMaster.GetAndResetObtainedStars();

        isCompleted = (obtainedStars > 0 ? true : false);
    }

    void PlayAfterLevelTrack()
    {
        var audioSource = GetComponent<AudioSource>();
        if (isCompleted)
        {
            audioSource.clip = winTrack;
        }
        else
        {
            audioSource.clip = loseTrack;
        }
        audioSource.Play();
    }

    void SetupBadge()
    {
        if (isCompleted)
        {
            successBadge.SetActive(true);
            failBadge.SetActive(false);
        }
        else
        {
            successBadge.SetActive(false);
            failBadge.SetActive(true);
        }
    }

    void SetupTexts()
    {
        successfulOrdersText.text = "x" + successfulOrders.ToString();
        failedOrdersText.text = "x" + failedOrders.ToString();
        totalPointsText.text = totalPoints.ToString();
    }

    void SetupStars()
    {
        if (obtainedStars > 0)
        {
            star1.SetActive(true);
        }

        if (obtainedStars > 1)
        {
            star2.SetActive(true);
        }

        if (obtainedStars > 2)
        {
            star3.SetActive(true);
        }
    }

    void SaveLevelProgress()
    {
        var gameMaster = FindObjectOfType<GameMaster>();

        var levelData = gameMaster.GetLevelData(levelName);

        bool hasBeenCompletedNew = true;
        int highscoreNew = (totalPoints > levelData.GetHighscore() ? totalPoints : levelData.GetHighscore());
        int obtainedStarsNew = (obtainedStars > levelData.GetObtainedStar() ? obtainedStars : levelData.GetObtainedStar());

        gameMaster.UpdateLevelDataAndSaveGame(levelName, hasBeenCompletedNew, highscoreNew, obtainedStarsNew);
    }

    public bool IsCompleted() => isCompleted;
}
