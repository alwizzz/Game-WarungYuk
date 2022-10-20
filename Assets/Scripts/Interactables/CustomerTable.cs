using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerTable : Table
{
    [SerializeField] CustomerAction customerAction;
    [SerializeField] bool hasCustomer;

    [SerializeField] UncompletedDish basePlateDishPrefab;
    [SerializeField] UncompletedDish baseBowlDishPrefab;

    [SerializeField] Vector3 bubblePosition;
    [SerializeField] Quaternion bubbleRotation;
    [SerializeField] RecipeBubble recipeBubble;

    private void Start()
    {
        UpdateHasCustomer();

        rendererMaster = GetComponentInChildren<RendererMaster>();
        childRenderers = rendererMaster.GetChildRenderers();

        bubbleRotation = Quaternion.Euler(new Vector3(70f, 0f, 0f));
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
            itemOnTable = null;
            UpdateHasItemOnTable();

            customerAction = null;
            UpdateHasCustomer();

            DespawnRecipeBubble();
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
        SpawnRecipeBubble(
            customerAction.GetOrderedDish().GetRecipeBubblePrefab(),
            customerAction.GetCustomerMovement()
        );
    }

    void SpawnBaseDishToTable(Dish.BaseDish baseDish)
    {
        UncompletedDish uDish = Instantiate(
            (baseDish == Dish.BaseDish.Plate ? basePlateDishPrefab : baseBowlDishPrefab),
            transform.position,
            Quaternion.identity
        );
        // Debug.Log("spawned " + uDish);

        itemOnTable = uDish;
        itemOnTable.MoveToPivot(placingPivot);
        UpdateHasItemOnTable();
    }

    void SpawnRecipeBubble(RecipeBubble recipeBubblePrefab, CustomerMovement cm)
    {
        recipeBubble = Instantiate(
            recipeBubblePrefab,
            bubblePosition + transform.position,
            bubbleRotation
        );
        recipeBubble.transform.SetParent(transform);
        recipeBubble.Setup(cm);
    }

    void DespawnRecipeBubble()
    {
        Destroy(recipeBubble.gameObject);
        recipeBubble = null;
    }


    void UpdateHasCustomer() { hasCustomer = (customerAction ? true : false); }
}
