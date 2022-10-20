using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBubble : MonoBehaviour
{
    [SerializeField] bool hasBeenSetup;
    [SerializeField] Transform angryProgressPivot;
    [SerializeField] CustomerMovement customerMovement; //where isAngry placed
    float toBeAngryDuration;

    private void Awake()
    {
        hasBeenSetup = false;
    }

    private void FixedUpdate()
    {
        if(hasBeenSetup && !customerMovement.IsAngry())
        {
            float normalizedProgress = (customerMovement.GetAngryCounter() / toBeAngryDuration);
            angryProgressPivot.localScale = new Vector3(normalizedProgress, 1f, 1f);
        }
    }

    public void Setup(CustomerMovement cm)
    {
        customerMovement = cm;
        toBeAngryDuration = customerMovement.GetToBeAngryDuration();
        hasBeenSetup = true;
    }
}
