using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : Interactable
{

    [SerializeField] LevelMaster levelMaster;
    [SerializeField] CookingRecipe cookingRecipe;
    [SerializeField] CookingRecipe.DishState basePlateDishState;
    [SerializeField] CookingRecipe.DishState baseBowlDishState;

    private void Awake()
    {
        levelMaster = FindObjectOfType<LevelMaster>();
    }

    private void Start()
    {
        cookingRecipe = levelMaster.GetCookingRecipe();
        basePlateDishState = cookingRecipe.GetBowlDishStates().Find((ds) => ds.name == "mangkok");
        basePlateDishState = cookingRecipe.GetPlateDishStates().Find((ds) => ds.name == "piring");
    }

    public override void InteractionWhenPlayerHoldingItem(PlayerAction playerAction)
    {
        var playerHeldItem = playerAction.GetHeldItem();
        var playerItemTypeString = playerHeldItem.GetType().ToString();
        if(playerItemTypeString == "UncompletedDish")
        {
            UncompletedDish playerUDish = (UncompletedDish)playerHeldItem;
            playerUDish.ClearMixedIngredient();
            Debug.Log("mubazir bro");
        }
        else if(playerItemTypeString == "RawIngredient" || playerItemTypeString == "ProcessedIngredient")
        {
            DestroyItemFromPlayer(playerAction);
        }
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
