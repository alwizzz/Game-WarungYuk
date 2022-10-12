using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processor : Interactable
{
    public override void InteractionWhenPlayerHoldingItem(PlayerAction playerAction)
    {
        var playerItemTypeString = playerAction.GetHeldItemTypeString();
        Debug.Log("held item is " + playerItemTypeString);
        if(playerItemTypeString == "RawIngredient")
        {
            ProcessIngredient();
        }
        else if (playerItemTypeString == "UncompletedDish")
        {
            ProcessDish();
        }
    }

    void ProcessIngredient()
    {
        Debug.Log("process ingredient");
    }

    void ProcessDish()
    {
        Debug.Log("process dish");
    }
}
