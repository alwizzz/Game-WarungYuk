using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnerMaster : MonoBehaviour
{
    [SerializeField] List<CompletedDish> completedDishPrefabs;
    [SerializeField] List<int> completedDishSpawnCounters;
    LevelMaster levelMaster;

    private void Awake()
    {
        levelMaster = FindObjectOfType<LevelMaster>();
    }

    private void Start()
    {
        completedDishPrefabs = levelMaster.GetCompletedDishPrefabs();
    }
}
