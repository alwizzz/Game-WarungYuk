using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopper : MonoBehaviour
{
    [SerializeField] int counter;

    [SerializeField] bool isForwardStopper = false;
    [SerializeField] bool isBackwardStopper = false;
    [SerializeField] bool isLeftStopper = false;
    [SerializeField] bool isRightStopper = false;

    PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        counter = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("enter");
        counter++;
        
        if (isForwardStopper) { playerMovement.SetBlockedForward(true); }
        else if (isBackwardStopper) { playerMovement.SetBlockedBackward(true); }
        else if (isLeftStopper) { playerMovement.SetBlockedLeft(true); }
        else if (isRightStopper) { playerMovement.SetBlockedRight(true); }

    }

    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("exit");
        counter--;
        if(counter > 0) { return; }
        
        if (isForwardStopper) { playerMovement.SetBlockedForward(false); }
        else if (isBackwardStopper) { playerMovement.SetBlockedBackward(false); }
        else if (isLeftStopper) { playerMovement.SetBlockedLeft(false); }
        else if (isRightStopper) { playerMovement.SetBlockedRight(false); }

    }
}
