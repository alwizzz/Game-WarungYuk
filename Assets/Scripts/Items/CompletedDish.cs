using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletedDish : Dish
{
    [SerializeField] int point = 15000;
    public int GetPoint() => point;
}
