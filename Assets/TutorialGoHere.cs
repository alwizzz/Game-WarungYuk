using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TutorialGoHere : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private UnityEvent callbackOnFinished;
    [SerializeField] private float distance;
    [SerializeField] private float threshold;


    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        if(distance < threshold)
        {
            callbackOnFinished?.Invoke();
        }
    }
}

//if (FindObjectOfType<GameMaster>().OnTutorial())
//{

//}
//else
//{

//}