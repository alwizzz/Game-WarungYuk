using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletedDish : Dish
{
    [SerializeField] int point = 15000;
    [SerializeField] RecipeBubble recipeBubblePrefab;
    public int GetPoint() => point;
    public RecipeBubble GetRecipeBubblePrefab() => recipeBubblePrefab;

}
