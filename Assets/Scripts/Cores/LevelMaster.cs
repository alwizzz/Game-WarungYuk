using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    [SerializeField] TextAsset jsonFile;
    [SerializeField] CookingRecipe cookingRecipe;

    private void Awake()
    {
        cookingRecipe = JsonUtility.FromJson<CookingRecipe>(jsonFile.ToString());
        Debug.Log(cookingRecipe);
    }
}
