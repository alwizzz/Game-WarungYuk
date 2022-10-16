using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] CompletedDish orderedDish;

    public CompletedDish GetOrderedDish() => orderedDish;
}
