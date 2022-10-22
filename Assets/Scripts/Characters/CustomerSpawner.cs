using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] List<CustomerAction> customerPrefabs;
    [SerializeField] CustomerAction previousSpawnedPrefab;
    [SerializeField] bool hasExistingSpawn;
    [SerializeField] bool hasArrived;
    [SerializeField] bool hasSpawnedInitial;
    //[SerializeField] float initialSpawnDelayMin;
    //[SerializeField] float initialSpawnDelayMax;
    [SerializeField] float spawnDelayMin;
    [SerializeField] float spawnDelayMax;

    LevelMaster levelMaster;

    private void Awake()
    {
        levelMaster = FindObjectOfType<LevelMaster>();
    }

    private void Start()
    {
        Setup();
    }

    void Setup()
    {
        //initialSpawnDelayMin = levelMaster.GetInitialSpawnDelayMin();
        //initialSpawnDelayMax = levelMaster.GetInitialSpawnDelayMax();
        //initialSpawnDelayMultiplier = levelMaster.GetInitialSpawnDelayMultiplier();
        spawnDelayMin = levelMaster.GetSpawnDelayMin();
        spawnDelayMax = levelMaster.GetSpawnDelayMax();

        hasExistingSpawn = false;
        hasSpawnedInitial = false;
    }

    private void Update()
    {
        if (levelMaster.GameHasStarted() && hasSpawnedInitial && !hasExistingSpawn)
        {
            StartCoroutine(
                Spawn(Random.Range(spawnDelayMin, spawnDelayMax))
            );
        }
    }

    IEnumerator Spawn(float delay, bool isSpawnInitial = false)
    {
        SetHasExistingSpawn(true);
        var prefabToBeSpawned = GetRandomizedPrefab(); ;
        
        yield return new WaitForSecondsRealtime(delay);
        var spawn = Instantiate(
            prefabToBeSpawned,
            transform.position,
            Quaternion.identity
        );

        spawn.SetOrderedDish(levelMaster.GetRandomCompletedDishPrefab());
        var spawnMovement = spawn.GetCustomerMovement();
        spawnMovement.SetCustomerSpawner(this);
        spawnMovement.SetAngryConfig(levelMaster.GetToBeAngryDuration());
        spawn.transform.SetParent(transform);

        if (isSpawnInitial) { hasSpawnedInitial = true; }
    }

    public void InitialSpawn()
    {
        StartCoroutine(Spawn(0, true));
    }

    CustomerAction GetRandomizedPrefab()
    {
        CustomerAction prefabToBeSpawned;
        do {
            prefabToBeSpawned = customerPrefabs[Random.Range(0, customerPrefabs.Count)];
        } while (prefabToBeSpawned == previousSpawnedPrefab);

        previousSpawnedPrefab = prefabToBeSpawned;
        return prefabToBeSpawned;
    }

    public bool HasArrived() => hasArrived;
    public void SetHasArrived(bool value) { hasArrived = value; }
    public void SetHasExistingSpawn(bool value) { hasExistingSpawn = value; }
}
