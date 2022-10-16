using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    [SerializeField] TextAsset jsonFile;
    [SerializeField] CookingRecipe cookingRecipe;
    [SerializeField] List<CompletedDish> completedDishPrefabs;
    [SerializeField] List<RawIngredient> rawIngredientPrefabs;
    [SerializeField] List<ProcessedIngredient> processedIngredientPrefabs;


    private void Awake()
    {
        cookingRecipe = JsonUtility.FromJson<CookingRecipe>(jsonFile.ToString());
        //Debug.Log(cookingRecipe);
    }

    public CookingRecipe GetCookingRecipe() => cookingRecipe;

    public CompletedDish GetCompletedDishPrefab(string codeName)
    {
        var cDish = completedDishPrefabs.Find((cd) => cd.GetCodeName() == codeName);

        //Debug.Log(codeName + " " + cDish);
        //completedDishPrefabs.ForEach((x) => Debug.Log(x.GetCodeName()));
        return cDish ? cDish : null;
    }
    public ProcessedIngredient GetProcessedIngredientPrefab(string codeName)
    {
        var pIng = processedIngredientPrefabs.Find((pIng) => pIng.GetCodeName() == codeName);

        //Debug.Log(codeName + " " + pIng);
        //completedDishPrefabs.ForEach((x) => Debug.Log(x.GetCodeName()));
        return pIng ? pIng : null;
    }
}
