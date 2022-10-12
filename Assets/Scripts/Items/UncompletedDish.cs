using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncompletedDish : Dish
{
    [SerializeField] static UncompletedDish platePrefab;
    [SerializeField] static UncompletedDish plateMixturePrefab;
    [SerializeField] static UncompletedDish bowlPrefab;
    [SerializeField] static UncompletedDish bowlMixturePrefab;

    public enum DishState
    {
        PlateEmpty,
        PlateMixture,
        BowlEmpty,
        BowlMixture
    }

    [SerializeField] DishState currentDishState;
    [SerializeField] Ingredient[] mixedIngredients;

    UncompletedDish(DishState dishState)
    {
        currentDishState = dishState;
    }

}
