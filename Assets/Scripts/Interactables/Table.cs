using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Interactable
{
    [SerializeField] Transform placingPivot;
    [SerializeField] Item itemOnTable;
    [SerializeField] bool hasItemOnTable;

    private void Start()
    {
        UpdateHasItemOnTable();
        if (hasItemOnTable) { itemOnTable.MoveToPivot(placingPivot); }
    }

    //public override void Interact(PlayerAction playerAction)
    //{
    //    if (hasItemOnTable)
    //    {
    //        if (playerAction.IsHolding())
    //        {
    //            Debug.Log("player attempts to put item to table but there's something on top of table!");
    //        } else
    //        {
    //            Debug.Log("player attempts to take item from table and it works");
    //        }
    //    } else
    //    {
    //        if (playerAction.IsHolding())
    //        {
    //            Debug.Log("player attempts to put item to table and it works");
    //        }
    //        else
    //        {
    //            Debug.Log("player attempts to take item from table but there's nothing on top of table!");
    //        }
    //    }
    //}

    public override void InteractionWhenPlayerHoldingItem(PlayerAction playerAction)
    {
        if(hasItemOnTable) { return; } // do nothing if there's something on table
        TakeItemFromPlayer(playerAction);
        //TakeItemFromPlayerREF(playerAction, itemOnTable, plac);

    }

    public override void InteractionWhenPlayerNotHoldingItem(PlayerAction playerAction)
    {
        if (!hasItemOnTable) { return; } // do nothing if there's nothing on table
        GiveItemToPlayer(playerAction);
    }

    void TakeItemFromPlayer(PlayerAction playerAction)
    {
        itemOnTable = playerAction.GetHeldItem();
        itemOnTable.MoveToPivot(placingPivot);
        UpdateHasItemOnTable();
    }

    // => NYOBA2
    //public override void TakeItemFromPlayer(PlayerAction playerAction, ref Item item)
    //{
    //    base.TakeItemFromPlayer(playerAction, ref item);

    //    item.MoveToPivot(placingPivot);
    //    UpdateHasItemOnTable();
    //}

    void GiveItemToPlayer(PlayerAction playerAction)
    {
        playerAction.SetHeldItem(itemOnTable);
        itemOnTable = null;
        UpdateHasItemOnTable();
    }



    public Vector3 GetPlacingPivot() => placingPivot.position;
    void UpdateHasItemOnTable() { hasItemOnTable = (itemOnTable ? true : false); }



}
