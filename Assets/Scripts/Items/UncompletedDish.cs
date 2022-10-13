using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncompletedDish : Dish
{
    [SerializeField] GameObject emptyPrefab;
    [SerializeField] GameObject mixturePrefab;

    public enum ContentState { Empty, Mixture }
    [SerializeField] ContentState currentContentState;

    [SerializeField] List<CookingRecipe.DishState> dishStates;
    //[SerializeField] CookingRecipe.DishState currentDishState;

    [SerializeField] List<Ingredient> mixedIngredients; //kepake buat ngasi info window


    [SerializeField] LevelMaster levelMaster;
    [SerializeField] CookingRecipe cookingRecipe;

    private void Awake()
    {
        levelMaster = FindObjectOfType<LevelMaster>();
    }

    private void Start()
    {
        cookingRecipe = levelMaster.GetCookingRecipe();

        if (codeName == "mangkok") 
        { 
            dishStates = cookingRecipe.GetBowlDishStates();
        }
        else if (codeName == "piring") 
        { 
            dishStates = cookingRecipe.GetPlateDishStates(); 
        }
        currentDishState = dishStates.Find((ds) => ds.name == codeName);

        UpdateContentState();
    }

    void UpdateContentState()
    {
        if(mixedIngredients.Count > 0)
        {
            currentContentState = ContentState.Mixture;
            emptyPrefab.SetActive(false);
            mixturePrefab.SetActive(true);
        }
        else
        {
            currentContentState = ContentState.Empty;
            emptyPrefab.SetActive(true);
            mixturePrefab.SetActive(false);
        }
    }

    public bool IsMixable(Ingredient ingredient)
    {
        var isMixable = false;
        var ingredientCodeName = ingredient.GetCodeName();
        //currentDishState.transitions.ForEach(
        //    (dt) =>
        //    {
        //        if(i.getCodeName() == dt.input)
        //        {
        //            isMixable = true;
        //            break;
        //        }
        //    }    
        //);
        foreach(CookingRecipe.DishState.DishTransition dt in currentDishState.transitions)
        {
            if(ingredientCodeName == dt.input)
            {
                isMixable = true;
                break;
            }
        }

        return isMixable;
    }

    public void Mix(Ingredient ingredient)
    {
        var dt = currentDishState.transitions.Find((dt) => dt.input == ingredient.GetCodeName());
        if (dt.intoCompletedDish)
        {
            //TODO
            Debug.Log("Welosssssss");
        }
        else
        {
            currentDishState = dishStates.Find((ds) => ds.name == dt.nextState);
            mixedIngredients.Add(ingredient);
            UpdateContentState();
        }

        ingredient.MoveToPivot(transform);
        ingredient.gameObject.SetActive(false);

    }
}
