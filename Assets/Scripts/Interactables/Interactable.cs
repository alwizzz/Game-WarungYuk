using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected RendererMaster rendererMaster;
    [SerializeField] protected List<RendererMaster.ChildRenderer> childRenderers;

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
            childRenderers.ForEach(
                (cr) =>
                {
                    //foreach(Material m in cr.materials)
                    //{
                    //    m.color = Color.red;
                    //}
                    cr.GetMaterials().ForEach(
                        (m) =>
                        {
                            m.color = (rendererMaster.HasChildren() ? Color.red : Color.yellow);
                        }
                    );
                
                }    
            );
        }  
        else
        {
            //childRenderers.ForEach(
            //    (cr) =>
            //    {
            //        foreach (Material m in cr.GetMaterials())
            //        {
            //            m.color = Color.clear;
            //        }
            //    }
            //);
            for (int i = 0; i<childRenderers.Count; i++)
            {
                var childRenderer = childRenderers[i];
                var materials = childRenderer.GetMaterials();
                for(int j = 0; j<materials.Count; j++)
                {
                    materials[j].color = childRenderer.GetDefaultMaterialColors()[j];
                }
            }
        }
    }
}
