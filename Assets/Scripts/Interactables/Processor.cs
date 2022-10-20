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

    [SerializeField] ProgressBar progressBar;
    [SerializeField] float processDuration;
    [SerializeField] float timeCounter;

    IEnumerator processDishHandler;
    [SerializeField] bool hasAnimation;
    [SerializeField] Animator processorAnimator;
    [SerializeField] GameObject idleModel;
    [SerializeField] GameObject processingModel;


    private void Awake()
    {
        levelMaster = FindObjectOfType<LevelMaster>();
        hasAnimation = (processorAnimator == null ? false : true);
    }

    private void Start()
    {
        cookingRecipe = levelMaster.GetCookingRecipe();
        //StartCoroutine(Delay(5));
        dishProcessTransitions = cookingRecipe.GetDishProcessTransitions(processName);
        ingredientProcessTransitions = cookingRecipe.GetIngredientProcessTransitions(processName);

        UpdateIsProcessing();
        ResetCounter();

        rendererMaster = GetComponentInChildren<RendererMaster>();
        childRenderers = rendererMaster.GetChildRenderers();
    }

    public void Process(PlayerAction playerAction)
    {
        var playerHeldItem = playerAction.GetHeldItem();
        var playerHeldItemTypeString = playerHeldItem?.GetType().ToString();
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
        var ipt = ingredientProcessTransitions.Find((ipt) => ipt.input == ing.GetCodeName());
        if(ipt != null)
        {
            processDishHandler = Processing(playerAction, ipt, false);
            StartCoroutine(processDishHandler);
        }
    }

    void AttemptToProcessDish(PlayerAction playerAction, UncompletedDish uDish)
    {
        var uDishState = uDish.GetCurrentDishState().name;
        var dpt = dishProcessTransitions.Find((dpt) => dpt.input == uDishState);
        if(dpt != null)
        {
            processDishHandler = Processing(playerAction, dpt, true);
            StartCoroutine(processDishHandler);
        }
    }

    IEnumerator Processing(PlayerAction playerAction, CookingRecipe.ProcessTransition pt, bool isProcessingDish)
    {
        //Debug.Log("oyoyoy");
        BeforeProcess(playerAction, pt.output, isProcessingDish);

        while(timeCounter < processDuration)
        {
            yield return null;
            timeCounter += Time.deltaTime;
        }
        Debug.Log("progress done...");

        Destroy(playerAction.TakeHeldItem().gameObject);
        GiveProcessorOutputToPlayer(playerAction, inProcessItem, isProcessingDish);

        AfterProcess(playerAction);
    }

    void BeforeProcess(PlayerAction playerAction, string ptOutput, bool isProcessingDish)
    {
        playerAction.SetIsUsingProcessor(true);
        playerAction.HideHeldItem();
        if (isProcessingDish)
        {
            inProcessItem = levelMaster.GetCompletedDishPrefab(ptOutput);
        } 
        else // is processing ingredient
        {
            inProcessItem = levelMaster.GetProcessedIngredientPrefab(ptOutput);
        }
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

    void GiveProcessorOutputToPlayer(PlayerAction playerAction, Item processorOutputPrefab, bool isProcessingDish)
    {

        Item output = Instantiate(processorOutputPrefab, transform.position, Quaternion.identity);

        if (isProcessingDish)
        {
            playerAction.GiveItemToHold((CompletedDish)output);
        }
        else // is processing ingredient
        {
            playerAction.GiveItemToHold((ProcessedIngredient)output);
        }
    }

    void UpdateIsProcessing() 
    {
        if (inProcessItem)
        {
            isProcessing = true;
            progressBar.Show();
        } 
        else
        {
            isProcessing = false;
            progressBar.Hide();
        }

        if (hasAnimation) { processorAnimator.SetBool("isProcessing", isProcessing); }
        else { UpdateActiveModel(); }
    }

    void ResetCounter() { timeCounter = 0; }

    public bool IsProcessing() => isProcessing;
    public float GetTimeCounter() => timeCounter;
    public float GetProcessDuration() => processDuration;
    public void SetProgressBar(ProgressBar obj) { progressBar = obj; }
    void UpdateActiveModel()
    {
        if (isProcessing)
        {
            idleModel.SetActive(false);
            processingModel.SetActive(true);
        }
        else
        {
            idleModel.SetActive(true);
            processingModel.SetActive(false);
        }
    }

}
