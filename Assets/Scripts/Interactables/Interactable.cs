using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //[SerializeField] protected RendererMaster rendererMaster;
    [SerializeField] protected List<Renderer> modelRenderers;

    public void Interact(PlayerAction playerAction)
    {
        //Debug.Log("player attempts to interact with " + this);
        if (playerAction.IsHolding())
        {
            InteractionWhenPlayerHoldingItem(playerAction);
        } else
        {
            InteractionWhenPlayerNotHoldingItem(playerAction);
        }
    }

    public virtual void InteractionWhenPlayerHoldingItem(PlayerAction playerAction)
    {
        return;
    }
    public virtual void InteractionWhenPlayerNotHoldingItem(PlayerAction playerAction)
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

    public void SetHighlighted(bool isHighlighted)
    {
        if (isHighlighted)
        {
            modelRenderers.ForEach(
                (mr) =>
                {
                    foreach(Material m in mr.materials)
                    {
                        m.color = Color.red;
                    }
                }    
            );
        }  
        else
        {
            modelRenderers.ForEach(
                (mr) =>
                {
                    foreach (Material m in mr.materials)
                    {
                        m.color = Color.clear;
                    }
                }
            );
        }
    }
}
