using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    [SerializeField] TextAsset jsonFile;
    [SerializeField] CookingRecipe cookingRecipe;
    [SerializeField] CompletedDish[] completedDishPrefabs;
    [SerializeField] RawIngredient[] rawIngredientPrefabs;
    [SerializeField] ProcessedIngredient[] processedIngredientPrefabs;


    private void Awake()
    {
        cookingRecipe = JsonUtility.FromJson<CookingRecipe>(jsonFile.ToString());
        Debug.Log(cookingRecipe);
    }

    public CookingRecipe GetCookingRecipe() => cookingRecipe;

}
