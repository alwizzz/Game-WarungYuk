using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] float levelDuration;
    [SerializeField] float timeCounter;
    IEnumerator startTimerHandler;
    [SerializeField] bool isTimeUp;
    [SerializeField] bool isCounting;
    [SerializeField] TextMeshProUGUI timerText;


    private void Awake()
    {
        startTimerHandler = StartTimer();
        isCounting = false;
    }
    public void ContinueTimer()
    {
        if (isTimeUp) { return; }
        StartCoroutine(startTimerHandler);
        isCounting = true;
    }
    public void PauseTimer()
    {
        if (isTimeUp) { return; }
        StopCoroutine(startTimerHandler);
        isCounting = false;
    }

    void UpdateIsTimeUp() { isTimeUp = timeCounter > 0 ? false : true; }
    void UpdateTimerDisplay()
    {
        timerText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(timeCounter / 60), Mathf.FloorToInt(timeCounter % 60));
    }
    public bool IsTimeUp() => isTimeUp;
    public bool IsCounting() => isCounting;

    IEnumerator StartTimer()
    {
        do
        {
            yield return null;
            timeCounter -= Time.deltaTime;
            UpdateIsTimeUp();
            UpdateTimerDisplay();
        } while (!isTimeUp);
        Debug.Log("LEVEL TIMER: TIME'S UP");
        timeCounter = 0f;
        UpdateIsTimeUp();
        UpdateTimerDisplay();
    }

    public void ResetCounter()
    {
        timeCounter = levelDuration;
        UpdateIsTimeUp();
    }

    public void SetLevelDuration(float value) 
    { 
        levelDuration = value;
        ResetCounter();
    }
}
