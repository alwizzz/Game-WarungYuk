using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : Interactable
{
    public override void InteractionWhenPlayerHoldingItem(PlayerAction playerAction)
    {
        DestroyItemFromPlayer(playerAction);
    }

    void DestroyItemFromPlayer(PlayerAction playerAction)
    {
        var item = playerAction.TakeHeldItem();
        //if(item is Dish)
        //{

        //} else if(item is Ingredient)
        //{

        //}

        item.gameObject.SetActive(false);
        item.MoveToPivot(transform);
        Destroy(item);
    }
}
