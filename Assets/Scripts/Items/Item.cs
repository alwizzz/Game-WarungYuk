using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public void MoveToPivot(Transform pivotObject) 
    {
        transform.parent = pivotObject;
        transform.position = pivotObject.position; 
    }
}
