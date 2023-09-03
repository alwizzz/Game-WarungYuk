using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Interactable
{
    [SerializeField] RawIngredient rawIngredientPrefab;

    private void Awake()
    {
        levelSFXManager = FindObjectOfType<LevelSFXManager>();
    }
    private void Start()
    {
        rendererMaster = GetComponentInChildren<RendererMaster>();
        childRenderers = rendererMaster.GetChildRenderers();
    }
    public override void InteractionWhenPlayerNotHoldingItem(PlayerAction playerAction)
    {
        // tutorial inject
        if (FindObjectOfType<GameMaster>().OnTutorial())
        {
            var tutorialManager = FindObjectOfType<TutorialManager>();
            if (rawIngredientPrefab.GetCodeName() == "daging" && tutorialManager.GetState() == "TakeDaging")
            {
                tutorialManager.NextTutorialState("TakeDaging");

                levelSFXManager.PlayTakeItemFromSpawnerSFX();
                SpawnItemToPlayer(playerAction);
            } else if(rawIngredientPrefab.GetCodeName() == "mie" && tutorialManager.GetState() == "MixMie")
            {
                levelSFXManager.PlayTakeItemFromSpawnerSFX();
                SpawnItemToPlayer(playerAction);
            } else 
            {
                print("cant take " + rawIngredientPrefab.GetCodeName() + " yet...");
            }

            return;
        }

        levelSFXManager.PlayTakeItemFromSpawnerSFX();
        SpawnItemToPlayer(playerAction);
    }

    void SpawnItemToPlayer(PlayerAction playerAction)
    {
        var rawIngredient = Instantiate(
            rawIngredientPrefab,
            transform.position,
            Quaternion.identity
        );
        playerAction.GiveItemToHold(rawIngredient);
    }
}
