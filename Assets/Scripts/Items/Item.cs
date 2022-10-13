using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected string codeName;
    public void MoveToPivot(Transform pivotObject) 
    {
        transform.parent = pivotObject;
        transform.position = pivotObject.position; 
    }

    public string GetCodeName() => codeName;
}
