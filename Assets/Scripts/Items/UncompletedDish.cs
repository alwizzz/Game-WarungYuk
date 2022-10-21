using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncompletedDish : Dish
{
    [SerializeField] GameObject emptyPrefab;
    [SerializeField] GameObject mixturePrefab;
    [SerializeField] Transform mixedIngredientStash;
    [SerializeField] MixtureIconMaster icons;

    public enum ContentState { Empty, Mixture }
    [SerializeField] ContentState currentContentState;

    [SerializeField] List<CookingRecipe.DishState> dishStates;
    [SerializeField] CookingRecipe.DishState baseDishState;

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
        baseDishState = dishStates.Find((ds) => ds.name == codeName);
        currentDishState = baseDishState;

        UpdateContentState();
        
        if(currentContentState == ContentState.Empty) { } // dummy so that currentContentState not marked as unused
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

    public void Mix(Ingredient ingredient, Table table)
    {
        var dt = currentDishState.transitions.Find((dt) => dt.input == ingredient.GetCodeName());
        if (dt.intoCompletedDish)
        {
            table.ConvertIntoCompletedDish(dt.nextState);
        }
        else
        {
            currentDishState = dishStates.Find((ds) => ds.name == dt.nextState);
            mixedIngredients.Add(ingredient);
            UpdateContentState();
            icons.AddIcon(ingredient.GetMixtureIconPrefab());
        }

        ingredient.HideTo(mixedIngredientStash);
        ingredient.gameObject.SetActive(false);
    }

    public void ClearMixedIngredient()
    {
        currentDishState = baseDishState;
        mixedIngredients.Clear();
        foreach(Transform child in mixedIngredientStash)
        {
            Destroy(child.gameObject);
        }
        UpdateContentState();
        icons.ClearIcon();
    }

    public CookingRecipe.DishState GetCurrentDishState() => currentDishState;
}
