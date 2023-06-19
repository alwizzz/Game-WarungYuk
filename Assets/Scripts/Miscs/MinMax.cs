using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MinMaxFloat
{

    [SerializeField] private float minimum;
    [SerializeField] private float maximum;
    [SerializeField] private float counter;


    public float GetRandomValue()
    {
        return Random.Range(minimum, maximum);
    }

    public float GetRandomValue(float divident)
    {
        return Random.Range(minimum/divident, maximum/divident);
    }

    public IEnumerator CountDown(UnityAction callback)
    {
        counter = GetRandomValue();
        
        while(counter > 0f)
        {
            yield return null;
            counter -= Time.deltaTime;
        }

        callback.Invoke();
    }

    public IEnumerator CountDownFaster(float divident)
    {
        counter = GetRandomValue(divident);

        while (counter > 0f)
        {
            yield return null;
            counter -= Time.deltaTime;
        }
    }
}

public class MinMaxInt
{
    [SerializeField] private int minimum;
    [SerializeField] private int maximum;

    public int GetRandomValue()
    {
        return Mathf.Max(minimum, maximum);
    }
}
