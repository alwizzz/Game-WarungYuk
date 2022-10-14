using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Item
{
    //[SerializeField] Ingredient handler;
    //[SerializeField] RawIngredient rawIngredientPrefab;

    //// TODO: RawIngredient should be able to be processed into multiple ProcessedIngredient, not just one
    //[SerializeField] bool isProcessable;
    //[SerializeField] ProcessedIngredient processedIngredientPrefab;
    //[SerializeField] string processorName;
    //private void Start()
    //{
    //    isProcessable = (processedIngredientPrefab ? true : false);
    //    handler = (rawIngredientPrefab ? rawIngredientPrefab : null);

    //    //if(handler == null) { return; }
    //    //else if(handler is RawIngredient) { raw = (RawIngredient)handler; }
    //    //else if (handler is ProcessedIngredient) { processed = (ProcessedIngredient)handler; }

    //    //Debug.Log((raw is Ingredient ? "oy" : "welos"));
    //    //Debug.Log(GetType());
    //    //Debug.Log(GetType().ToString() == "Ingredient");


    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q) && handler != null)
    //    {
    //        Debug.Log("oy");
    //        if(handler is RawIngredient)
    //        {
    //            rawIngredientPrefab.gameObject.SetActive(false);
    //            processedIngredientPrefab.gameObject.SetActive(true);
    //            handler = processedIngredientPrefab;
    //        } else if(handler is ProcessedIngredient)
    //        {
    //            processedIngredientPrefab.gameObject.SetActive(false);
    //            rawIngredientPrefab.gameObject.SetActive(true);
    //            handler = rawIngredientPrefab;
    //        }
    //    }
    //}
}
