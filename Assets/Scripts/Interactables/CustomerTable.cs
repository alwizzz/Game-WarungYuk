using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerTable : Table
{
    public override void InteractionWhenPlayerHoldingItem(PlayerAction playerAction)
    {
        if (!hasItemOnTable && playerAction.GetHeldItem().GetType().ToString() == "CompletedDish")
        {
            TakeItemFromPlayer(playerAction);
        }
    }
    public override void InteractionWhenPlayerNotHoldingItem(PlayerAction playerAction)
    {
        if (!hasItemOnTable) { return; } // do nothing if there's nothing on table
        GiveItemToPlayer(playerAction);
    }
}
