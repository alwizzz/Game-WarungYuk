using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processor : Interactable
{
    [SerializeField] string processName;
    [SerializeField] List<CookingRecipe.DishProcessTransition> dishProcessTransitions;
    [SerializeField] List<CookingRecipe.IngredientProcessTransition> ingredientProcessTransitions;

    [SerializeField] bool isProcessing;
    [SerializeField] Item inProcessItem;

    LevelMaster levelMaster;
    CookingRecipe cookingRecipe;

    [SerializeField] float processDuration;
    [SerializeField] float timeCounter;
    

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

        UpdateIsProcessing();
        ResetCounter();
    }

    //public override void InteractionWhenPlayerHoldingItem(PlayerAction playerAction)
    //{
    //    var playerHeldItem = playerAction.GetHeldItem();
    //    var playerHeldItemTypeString = playerHeldItem.GetType().ToString();
    //    Debug.Log("held item is " + playerHeldItemTypeString);
    //    if(playerHeldItemTypeString == "RawIngredient")
    //    {
    //        AttemptToProcessIngredient(playerAction, (Ingredient) playerHeldItem);
    //    }
    //    else if (playerHeldItemTypeString == "UncompletedDish")
    //    {
    //        AttemptToProcessDish(playerAction, (UncompletedDish)playerHeldItem);
    //    }
    //}

    public void Process(PlayerAction playerAction)
    {
        var playerHeldItem = playerAction.GetHeldItem();
        var playerHeldItemTypeString = playerHeldItem.GetType().ToString();
        Debug.Log("held item is " + playerHeldItemTypeString);
        if (playerHeldItemTypeString == "RawIngredient")
        {
            AttemptToProcessIngredient(playerAction, (Ingredient)playerHeldItem);
        }
        else if (playerHeldItemTypeString == "UncompletedDish")
        {
            AttemptToProcessDish(playerAction, (UncompletedDish)playerHeldItem);
        }
    }

    void AttemptToProcessIngredient(PlayerAction playerAction, Ingredient ing)
    {
        Debug.Log("process ingredient");
    }

    void AttemptToProcessDish(PlayerAction playerAction, UncompletedDish uDish)
    {
        var uDishState = uDish.GetCurrentDishState().name;
        var dpt = dishProcessTransitions.Find((dpt) => dpt.input == uDishState);
        if(dpt != null)
        {
            StartCoroutine(ProcessDish(playerAction, dpt));
        }
    }

    IEnumerator ProcessDish(PlayerAction playerAction, CookingRecipe.DishProcessTransition dpt)
    {
        Debug.Log("oyoyoy");
        playerAction.SetIsProcessing(true);
        playerAction.HideHeldItem();

        inProcessItem = levelMaster.GetCompletedDishPrefab(dpt.output);
        UpdateIsProcessing();

        while(timeCounter < processDuration)
        {
            timeCounter += Time.deltaTime;
            yield return null;
        }



        Destroy(playerAction.TakeHeldItem().gameObject);
        GiveCompletedDishToPlayer(playerAction, (CompletedDish)inProcessItem);

        playerAction.SetIsProcessing(false);
        inProcessItem = null;
        UpdateIsProcessing();
    }

    void GiveCompletedDishToPlayer(PlayerAction playerAction, CompletedDish cDish)
    {
        playerAction.GiveItemToHold(cDish);
    }

    void UpdateIsProcessing() { isProcessing = (inProcessItem ? true : false); }

    void ResetCounter() { timeCounter = 0; }
}
