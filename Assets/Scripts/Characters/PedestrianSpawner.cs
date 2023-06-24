using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PedestrianSpawner : MonoBehaviour
{
    [SerializeField] private bool isSpawning = true;
    [SerializeField] private List<PedestrianMovement> pedestrianPrefabs;
    [SerializeField] private PedestrianMovement lastPedestrianPrefab;

    [SerializeField] private MinMaxFloat spawnDelay;

    private void Start()
    {
        Setup();
        StartCoroutine(StartSpawning());
    }

    private void Setup()
    {
        lastPedestrianPrefab = null;
    }

    private IEnumerator StartSpawning()
    {
        // initial delay
        yield return new WaitForSeconds(spawnDelay.GetRandomValue(2f));

        Spawn();
    }

    public void Spawn()
    {
        if (!isSpawning) { return; }

        var prefabToBeSpawned = GetRandomizedPrefab();
        var spawn = Instantiate(
            prefabToBeSpawned,
            transform.position,
            Quaternion.identity
        );
        spawn.transform.parent = transform;
        lastPedestrianPrefab = spawn;

        StartCoroutine(spawnDelay.CountDown(Spawn));
    }

    private PedestrianMovement GetRandomizedPrefab()
    {
        var copy = new List<PedestrianMovement>(pedestrianPrefabs);
        copy.Remove(lastPedestrianPrefab);

        return copy[Random.Range(0, copy.Count - 1)];
    }
 
}
