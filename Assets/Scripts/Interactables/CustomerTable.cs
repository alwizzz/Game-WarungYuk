using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerTable : Table
{
    [SerializeField] Customer customer;
    [SerializeField] bool hasCustomer;

    [SerializeField] UncompletedDish basePlateDishPrefab;
    [SerializeField] UncompletedDish baseBowlDishPrefab;

    private void Start()
    {
        UpdateHasCustomer();
    }


    public override void InteractionWhenPlayerHoldingItem(PlayerAction playerAction)
    {
        if (!hasItemOnTable &&
            hasCustomer &&
            playerAction.GetHeldItem().GetType().ToString() == "CompletedDish" 
        )
        {
            TakeItemFromPlayer(playerAction);
            GiveOrderedDishToCustomer((CompletedDish)itemOnTable);
        }
    }
    public override void InteractionWhenPlayerNotHoldingItem(PlayerAction playerAction)
    {
        if (!hasItemOnTable) { return; } // do nothing if there's nothing on table
        GiveItemToPlayer(playerAction);
    }

    public void Order(Customer customer)
    {
        this.customer = customer;
        UpdateHasCustomer();
    }

    void GiveOrderedDishToCustomer(CompletedDish cDish)
    {

    }

    void UpdateHasCustomer() { hasCustomer = (customer ? true : false); }
}
