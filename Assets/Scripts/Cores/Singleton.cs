using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    [SerializeField] string type;
    private void Awake()
    {
        SingletonBehaviour();
    }

    void SingletonBehaviour()
    {
        int thisScriptCount;
        if(type == "GameMaster") { thisScriptCount = FindObjectsOfType<GameMaster>().Length; }
        else if(type == "SingletonLight") { thisScriptCount = FindObjectsOfType<SingletonLight>().Length; }
        else if(type == "ReflectionProbe") { thisScriptCount = FindObjectsOfType<ReflectionProbe>().Length; }
        else if (type == "AudioMaster") { thisScriptCount = FindObjectsOfType<AudioMaster>().Length; }
        else { Debug.Log("singleton error"); thisScriptCount = 99; }

        if (thisScriptCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
