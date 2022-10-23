using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRecipeBook : MonoBehaviour
{
    [SerializeField] RecipeBookMaster recipeBookMaster;

    public void OnClick()
    {
        recipeBookMaster.gameObject.SetActive(true);
        recipeBookMaster.OpenRecipeBook();

        gameObject.SetActive(false);
    }
}
