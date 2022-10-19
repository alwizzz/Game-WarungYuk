using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerTable : Table
{
    [SerializeField] CustomerAction customerAction;
    [SerializeField] bool hasCustomer;

    [SerializeField] UncompletedDish basePlateDishPrefab;
    [SerializeField] UncompletedDish baseBowlDishPrefab;

    private void Start()
    {
        UpdateHasCustomer();

        rendererMaster = GetComponentInChildren<RendererMaster>();
        childRenderers = rendererMaster.GetChildRenderers();
    }


    public override void InteractionWhenPlayerHoldingItem(PlayerAction playerAction)
    {
        if (!hasItemOnTable &&
            hasCustomer &&
            playerAction.GetHeldItem().GetType().ToString() == "CompletedDish" 
        )
        {
            TakeItemFromPlayer(playerAction);
            customerAction.TakeOrderedDishFromTable((CompletedDish)itemOnTable);
            UpdateHasItemOnTable();

            customerAction = null;
            UpdateHasCustomer();
        }
    }
    public override void InteractionWhenPlayerNotHoldingItem(PlayerAction playerAction)
    {
        if (!hasItemOnTable) { return; } // do nothing if there's nothing on table
        GiveItemToPlayer(playerAction);
        UpdateHasItemOnTable();
    }

    public void Order(CustomerAction customerAction)
    {
        this.customerAction = customerAction;
        UpdateHasCustomer();

        SpawnBaseDishToTable(customerAction.GetOrderedDish().GetBaseDish());
    }

    void SpawnBaseDishToTable(Dish.BaseDish baseDish)
    {
        UncompletedDish uDish = Instantiate(
            (baseDish == Dish.BaseDish.Plate ? basePlateDishPrefab : baseBowlDishPrefab),
            transform.position,
            Quaternion.identity
        );

        itemOnTable = uDish;
        itemOnTable.MoveToPivot(placingPivot);
        UpdateHasItemOnTable();
    }


    void UpdateHasCustomer() { hasCustomer = (customerAction ? true : false); }
}
