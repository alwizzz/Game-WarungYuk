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
