using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    [SerializeField] TextAsset jsonFile;
    CookingRecipe cookingRecipe;
    [SerializeField] List<CompletedDish> completedDishPrefabs;
    [SerializeField] List<ProcessedIngredient> processedIngredientPrefabs;

    [SerializeField] int totalPoint;


    private void Awake()
    {
        cookingRecipe = JsonUtility.FromJson<CookingRecipe>(jsonFile.ToString());
    }

    public CookingRecipe GetCookingRecipe() => cookingRecipe;

    public CompletedDish GetCompletedDishPrefab(string codeName)
    {
        var cDish = completedDishPrefabs.Find((cd) => cd.GetCodeName() == codeName);
        return cDish ? cDish : null;
    }
    public ProcessedIngredient GetProcessedIngredientPrefab(string codeName)
    {
        var pIng = processedIngredientPrefabs.Find((pIng) => pIng.GetCodeName() == codeName);
        return pIng ? pIng : null;
    }

    public void IncreasePoint(int value) { totalPoint += value; }
    public void DecreasePoint(int value)
    {
        totalPoint -= value;
        totalPoint = (totalPoint >= 0 ? totalPoint : 0);
    }
}
