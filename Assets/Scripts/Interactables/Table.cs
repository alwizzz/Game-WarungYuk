using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Interactable
{
    [SerializeField] protected Transform placingPivot;
    [SerializeField] protected Item itemOnTable;
    [SerializeField] protected bool hasItemOnTable;

    [SerializeField] private ParticleSystem unmixableVFX;

    [SerializeField] LevelMaster levelMaster;

    private void Awake()
    {
        levelMaster = FindObjectOfType<LevelMaster>();
        levelSFXManager = FindObjectOfType<LevelSFXManager>();
    }

    private void Start()
    {
        UpdateHasItemOnTable();
        if (hasItemOnTable) {
            itemOnTable = Instantiate(itemOnTable, transform.position, Quaternion.identity);
            itemOnTable.MoveToPivot(placingPivot); 
        }

        rendererMaster = GetComponentInChildren<RendererMaster>();
        childRenderers = rendererMaster.GetChildRenderers();
    }

    public override void InteractionWhenPlayerHoldingItem(PlayerAction playerAction)
    {
        if(!hasItemOnTable) 
        {
            levelSFXManager.PlayPopSFX();
            TakeItemFromPlayer(playerAction); 
        } 
        else
        {
            AttemptToMix(playerAction, playerAction.GetHeldItem());
        }
    }

    void AttemptToMix(PlayerAction playerAction, Item playerHeldItem)
    {
        var playerHeldItemString = playerHeldItem.GetType().ToString();
        if (
            itemOnTable.GetType().ToString() == "UncompletedDish" && 
            (playerHeldItemString == "RawIngredient" || playerHeldItemString == "ProcessedIngredient")
        )
        {
            // downcasting
            UncompletedDish uDish = (UncompletedDish)itemOnTable;
            Ingredient playerHeldIngredient = (Ingredient)playerHeldItem;

            if (uDish.IsMixable(playerHeldIngredient))
            {
                levelSFXManager.PlayMixSFX();

                var temp = playerAction.TakeHeldItem();
                Ingredient ingredientToMix = (Ingredient) temp;
                uDish.Mix(ingredientToMix, this);
            }
            else 
            {
                unmixableVFX.Play();
            }
        }
    }

    public void ConvertIntoCompletedDish(string cDishCodeName)
    {
        Destroy(itemOnTable.gameObject);

        var cDishPrefab = levelMaster.GetCompletedDishPrefab(cDishCodeName);
        CompletedDish cDish = Instantiate(
            cDishPrefab,
            transform.position,
            Quaternion.identity
        );

        cDish.MoveToPivot(placingPivot);
        itemOnTable = cDish;
        UpdateHasItemOnTable();
    }




    public override void InteractionWhenPlayerNotHoldingItem(PlayerAction playerAction)
    {
        if (!hasItemOnTable) { return; } // do nothing if there's nothing on table
        levelSFXManager.PlayPopSFX();
        GiveItemToPlayer(playerAction);
    }

    protected void TakeItemFromPlayer(PlayerAction playerAction)
    {
        itemOnTable = playerAction.TakeHeldItem();
        itemOnTable.MoveToPivot(placingPivot);
        UpdateHasItemOnTable();
    }

    protected void GiveItemToPlayer(PlayerAction playerAction)
    {
        playerAction.GiveItemToHold(itemOnTable);
        itemOnTable = null;
        UpdateHasItemOnTable();
    }

    protected void UpdateHasItemOnTable() { hasItemOnTable = (itemOnTable ? true : false); }



}
