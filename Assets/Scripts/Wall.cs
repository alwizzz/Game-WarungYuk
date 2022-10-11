using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] bool isForwardWall = false;
    [SerializeField] bool isBackwardWall = false;
    [SerializeField] bool isLeftWall = false;
    [SerializeField] bool isRightWall = false;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("enter");
        var playerMovement = collision.gameObject.GetComponentInParent<PlayerMovement>();

        if (isForwardWall) { playerMovement.SetBlockedForward(true); }
        else if (isBackwardWall) { playerMovement.SetBlockedBackward(true); }
        else if (isLeftWall) { playerMovement.SetBlockedLeft(true); }
        else if (isRightWall) { playerMovement.SetBlockedRight(true); }

    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("exit");

        var playerMovement = collision.gameObject.GetComponentInParent<PlayerMovement>();

        if (isForwardWall) { playerMovement.SetBlockedForward(false); }
        else if (isBackwardWall) { playerMovement.SetBlockedBackward(false); }
        else if (isLeftWall) { playerMovement.SetBlockedLeft(false); }
        else if (isRightWall) { playerMovement.SetBlockedRight(false); }

    }
}
