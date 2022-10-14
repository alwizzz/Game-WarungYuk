using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dish : Item
{
    [SerializeField] protected CookingRecipe.DishState currentDishState;

    public enum BaseDish { Plate, Bowl }
    [SerializeField] protected BaseDish baseDish;

    public BaseDish GetBaseDish() => baseDish;
}
