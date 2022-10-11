using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public void Interact(PlayerAction playerAction)
    {
        Debug.Log("player attempts to interact with " + this);
        if (playerAction.IsHolding())
        {
            InteractionWhenHoldingItem(playerAction);
        } else
        {
            InteractionWhenNotHoldingItem(playerAction);
        }
    }

    public virtual void InteractionWhenHoldingItem(PlayerAction playerAction)
    {
        return;
    }
    public virtual void InteractionWhenNotHoldingItem(PlayerAction playerAction)
    {
        return;
    }


    public void TouchedByPlayer()
    {
        Debug.Log(gameObject + " touched by player");
    }
    public void LeftByPlayer()
    {
        Debug.Log(gameObject + " left by player");
    }

    // ==> NYOBA2
    //public virtual void TakeItemFromPlayer(PlayerAction playerAction, ref Item item)
    //{
    //    item = playerAction.GetHeldItem();
    //}

    //public virtual void GiveItemToPlayer(PlayerAction playerAction, ref Item item)
    //{
    //    playerAction.SetHeldItem(item);
    //    item = null;
    //}


}
