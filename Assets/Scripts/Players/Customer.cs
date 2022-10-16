using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] CompletedDish orderedDish;

    public void TakeOrderedDishFromTable(CompletedDish givenDish)
    {
        if(givenDish.GetCodeName() == orderedDish.GetCodeName())
        {

        }
    }


    public CompletedDish GetOrderedDish() => orderedDish;
}
