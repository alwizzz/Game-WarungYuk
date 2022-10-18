using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] List<CustomerAction> customerPrefabs;
    [SerializeField] CustomerAction previousSpawnedPrefab;
    [SerializeField] bool hasExistingSpawn;
    [SerializeField] bool hasArrived;
    [SerializeField] float spawnDelay;

    private void Start()
    {
        StartCoroutine(Spawn(0f));
    }

    private void Update()
    {
        if (!hasExistingSpawn)
        {
            Debug.Log("spawning..");
            StartCoroutine(Spawn(spawnDelay));
        }
    }

    IEnumerator Spawn(float delay)
    {
        SetHasExistingSpawn(true);
        var prefabToBeSpawned = GetRandomizedPrefab(); ;
        
        yield return new WaitForSecondsRealtime(delay);
        var spawn = Instantiate(
            prefabToBeSpawned,
            transform.position,
            Quaternion.identity
        );
        spawn.GetCustomerMovement().SetCustomerSpawner(this);
        spawn.transform.SetParent(transform);

        
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
