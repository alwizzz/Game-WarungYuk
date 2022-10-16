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

    IEnumerator processDishHandler;


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
        //Debug.Log("held item is " + playerHeldItemTypeString);
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
            processDishHandler = ProcessDish(playerAction, dpt);
            StartCoroutine(processDishHandler);
        }
    }

    IEnumerator ProcessDish(PlayerAction playerAction, CookingRecipe.DishProcessTransition dpt)
    {
        //Debug.Log("oyoyoy");
        BeforeProcess(playerAction, dpt);

        while(timeCounter < processDuration)
        {
            timeCounter += Time.deltaTime;
            yield return null;
        }
        Debug.Log("progress done...");

        Destroy(playerAction.TakeHeldItem().gameObject);
        GiveCompletedDishToPlayer(playerAction, (CompletedDish)inProcessItem);


        AfterProcess(playerAction);
    }

    void BeforeProcess(PlayerAction playerAction, CookingRecipe.DishProcessTransition dpt)
    {
        playerAction.SetIsUsingProcessor(true);
        playerAction.HideHeldItem();
        inProcessItem = levelMaster.GetCompletedDishPrefab(dpt.output);
        UpdateIsProcessing();
    }

    void AfterProcess(PlayerAction playerAction)
    {
        inProcessItem = null;
        UpdateIsProcessing();
        ResetCounter();
        playerAction.SetIsUsingProcessor(false);

        processDishHandler = null;
    }

    public void AbortProcess(PlayerAction playerAction)
    {
        StopCoroutine(processDishHandler);
        playerAction.ShowHeldItem();
        AfterProcess(playerAction);
        Debug.Log("process aborted");
    }

    void GiveCompletedDishToPlayer(PlayerAction playerAction, CompletedDish cDishPrefab)
    {
        CompletedDish cDish = Instantiate(cDishPrefab, transform.position, Quaternion.identity);
        playerAction.GiveItemToHold(cDish);
    }

    void UpdateIsProcessing() { isProcessing = (inProcessItem ? true : false); }

    void ResetCounter() { timeCounter = 0; }
}
