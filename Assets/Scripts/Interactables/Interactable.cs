using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;



public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected RendererMaster rendererMaster;
    [SerializeField] protected List<RendererMaster.ChildRenderer> childRenderers;

    [SerializeField] protected LevelSFXManager levelSFXManager;

    [SerializeField] protected Color emissionColor = new Color32(60, 60, 30, 0);
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
                            //m.color = (rendererMaster.HasChildren() ? new Color32(50, 50, 50, 30) : new Color32(50, 50, 50, 50));
                            //m.color += (rendererMaster.HasChildren() ? colorForNonSpawner : colorForSpawner);
                            //m.EnableKeyword("_EMISSION");
                            m.EnableKeyword("_EMISSION");
                            m.SetColor("_EmissionColor", new Color32(60, 60, 30, 255));
                            //m.SetColor("_EmissionColor", Color.yellow);
                            //m.EnableKeyword(new LocalKeyword(m.shader, "_EMISSION"));


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
                    //materials[j].color = childRenderer.GetDefaultMaterialColors()[j];
                    var m = materials[j];
                    m.DisableKeyword("_EMISSION");
                }
            }
        }
    }
}
