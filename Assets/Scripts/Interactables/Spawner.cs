using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Interactable
{
    [SerializeField] Item rawIngredientPrefab;

    public override void InteractionWhenNotHoldingItem(PlayerAction playerAction)
    {
        SpawnItemToPlayer(playerAction);
    }

    void SpawnItemToPlayer(PlayerAction playerAction)
    {
        var rawIngredient = Instantiate(
            rawIngredientPrefab,
            transform.position,
            Quaternion.identity
        );
        playerAction.SetHeldItem(rawIngredient);
    }
}
