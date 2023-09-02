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
            var itemString = TakeItemFromPlayer(playerAction);

            // tutorial inject
            if (FindObjectOfType<GameMaster>().OnTutorial())
            {
                var tm = FindObjectOfType<TutorialManager>();
                if (itemString == "mangkok" && tm.GetState() == "DropPlate")
                {
                    tm.NextTutorialState("DropPlate");
                }
            }
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

                // tutorial inject
                if (FindObjectOfType<GameMaster>().OnTutorial())
                {
                    var tm = FindObjectOfType<TutorialManager>();
                    if (ingredientToMix.GetCodeName() == "daging" && tm.GetState() == "MixDaging")
                    {
                        tm.NextTutorialState("MixDaging");
                    } else if (ingredientToMix.GetCodeName() == "mie" && tm.GetState() == "MixMie")
                    {
                        tm.NextTutorialState("MixMie");
                    }
                }
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

    protected string TakeItemFromPlayer(PlayerAction playerAction)
    {
        itemOnTable = playerAction.TakeHeldItem();
        itemOnTable.MoveToPivot(placingPivot);
        UpdateHasItemOnTable();

        return itemOnTable.GetCodeName();
    }

    protected void GiveItemToPlayer(PlayerAction playerAction)
    {
        playerAction.GiveItemToHold(itemOnTable);
        itemOnTable = null;
        UpdateHasItemOnTable();
    }

    protected void UpdateHasItemOnTable() { hasItemOnTable = (itemOnTable ? true : false); }



}
