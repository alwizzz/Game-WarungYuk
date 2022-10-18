using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    [SerializeField] TextAsset jsonFile;
    CookingRecipe cookingRecipe;
    [SerializeField] List<ProcessedIngredient> processedIngredientPrefabs;
    [SerializeField] List<CompletedDish> completedDishPrefabs;
    [SerializeField] List<int> completedDishSpawnCounters;
    [SerializeField] bool stillHasCounters;

    [SerializeField] int totalPoint;
    [SerializeField] float wrongDishPenaltyMultiplier;
    [SerializeField] float customerIsAngryPenaltyMultiplier;


    [SerializeField] float initialSpawnDelayMin;
    [SerializeField] float initialSpawnDelayMax;
    [SerializeField] float spawnDelayMin;
    [SerializeField] float spawnDelayMax;

    [SerializeField] float toBeAngryDuration;

    private void Awake()
    {
        cookingRecipe = JsonUtility.FromJson<CookingRecipe>(jsonFile.ToString());
    }

    private void Start()
    {
        UpdateStillHasCounters();
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

    public void IncreasePoint(int dishPoint, bool customerIsAngry) 
    {
        if (customerIsAngry)
        {
            float temp = (customerIsAngryPenaltyMultiplier * dishPoint);
            temp /= 100f;
            int penaltiedPoint = Mathf.RoundToInt(temp) * 100;

            totalPoint += penaltiedPoint;
        }
        else
        {
            totalPoint += dishPoint; 
        }
    }
    public void DecreasePoint(int dishPoint)
    {
        float temp = (wrongDishPenaltyMultiplier * dishPoint);
        temp /= 100f;
        int penaltiedPoint = Mathf.RoundToInt(temp) * 100;

        totalPoint -= penaltiedPoint;
        totalPoint = (totalPoint >= 0 ? totalPoint : 0);
    }

    public List<CompletedDish> GetCompletedDishPrefabs() => completedDishPrefabs;
    public float GetInitialSpawnDelayMin() => initialSpawnDelayMin;
    public float GetInitialSpawnDelayMax() => initialSpawnDelayMax;
    public float GetSpawnDelayMin() => spawnDelayMin;
    public float GetSpawnDelayMax() => spawnDelayMax;
    public float GetToBeAngryDuration() => toBeAngryDuration;

    void UpdateStillHasCounters()
    {
        bool value = false;
        foreach (int x in completedDishSpawnCounters)
        {
            if(x > 0) {
                value = true;    
                break; 
            }

        }
        stillHasCounters = value;
    }

    public CompletedDish GetRandomCompletedDishPrefab()
    {
        int randomIndex = Random.Range(0, completedDishPrefabs.Count);
        if (stillHasCounters)
        {
            while(completedDishSpawnCounters[randomIndex] == 0)
            {
                randomIndex = Random.Range(0, completedDishPrefabs.Count);
            } 

            completedDishSpawnCounters[randomIndex] -= 1;
            UpdateStillHasCounters();
        }

        return completedDishPrefabs[randomIndex];
    }

}
