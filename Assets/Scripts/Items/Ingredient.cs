using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Item
{
    [SerializeField] MixtureIcon mixtureIconPrefab;

    public MixtureIcon GetMixtureIconPrefab() => mixtureIconPrefab;
}
