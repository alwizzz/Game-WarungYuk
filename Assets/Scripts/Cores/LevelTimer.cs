using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] float levelDuration;
    [SerializeField] float timeCounter;
    IEnumerator startTimerHandler;
    [SerializeField] bool isTimeUp;
    [SerializeField] bool isCounting;
    [SerializeField] string timerString;


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
    void UpdateTimerString()
    {
        timerString = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(timeCounter / 60), Mathf.FloorToInt(timeCounter % 60));
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
            UpdateTimerString();
        } while (!isTimeUp);
        Debug.Log("time counter reaches zero");
        timeCounter = 0f;
        UpdateIsTimeUp();
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
