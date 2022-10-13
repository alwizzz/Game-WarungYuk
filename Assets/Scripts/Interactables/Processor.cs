using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processor : Interactable
{
    [SerializeField] string processName;
    [SerializeField] List<CookingRecipe.DishProcessTransition> dishProcessTransitions;
    [SerializeField] List<CookingRecipe.IngredientProcessTransition> ingredientProcessTransitions;


    LevelMaster levelMaster;
    CookingRecipe cookingRecipe;
    

    private void Awake()
    {
        levelMaster = FindObjectOfType<LevelMaster>();

    }

    IEnumerator Delay(int n) { yield return new WaitForSeconds(n); }

    private void Start()
    {
        cookingRecipe = levelMaster.GetCookingRecipe();
        //StartCoroutine(Delay(5));
        dishProcessTransitions = cookingRecipe.GetDishProcessTransitions(processName);
        ingredientProcessTransitions = cookingRecipe.GetIngredientProcessTransitions(processName);
    }

    public override void InteractionWhenPlayerHoldingItem(PlayerAction playerAction)
    {
        var playerHeldItemTypeString = playerAction.GetHeldItem().GetType().ToString();
        Debug.Log("held item is " + playerHeldItemTypeString);
        if(playerHeldItemTypeString == "RawIngredient")
        {
            ProcessIngredient();
        }
        else if (playerHeldItemTypeString == "UncompletedDish")
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
