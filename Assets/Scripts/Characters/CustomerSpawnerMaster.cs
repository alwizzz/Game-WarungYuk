using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnerMaster : MonoBehaviour
{
    [SerializeField] float initialSpawnDelayMin;
    [SerializeField] float initialSpawnDelayMax;
    [SerializeField] List<CustomerSpawner> customerSpawners;
    List<CustomerSpawner> customerSpawnersUnspawned;
    LevelMaster levelMaster;
    bool hasEverSpawned;

    private void Awake()
    {
        customerSpawnersUnspawned = customerSpawners;
        levelMaster = FindObjectOfType<LevelMaster>();
        hasEverSpawned = false;
    }

    private void Start()
    {
        if (FindObjectOfType<GameMaster>().OnTutorial())
        {
            initialSpawnDelayMin = 0;
            initialSpawnDelayMax = 0;
        } else
        {
            initialSpawnDelayMin = levelMaster.GetInitialSpawnDelayMin();
            initialSpawnDelayMax = levelMaster.GetInitialSpawnDelayMax();
        }

          
    }

    private void Update()
    {
        if(levelMaster.GameHasStarted() && !hasEverSpawned)
        {
            hasEverSpawned = true;
            StartCoroutine(SequentialSpawn());
        }
    }

    IEnumerator SequentialSpawn()
    {
        bool hasFirstSpawned = false;
        while(customerSpawnersUnspawned.Count > 0)
        {
            var randomIndex = Random.Range(0, customerSpawnersUnspawned.Count);
            float delay;
            if (!hasFirstSpawned)
            {
                if (FindObjectOfType<GameMaster>().OnTutorial())
                {
                    delay = 0f;
                }
                else
                {
                    delay = Random.Range(2f, 5f);
                }
                hasFirstSpawned = true;
            }
            else
            {
                delay = Random.Range(initialSpawnDelayMin, initialSpawnDelayMax);
            }

            yield return new WaitForSecondsRealtime(delay);

            var customerSpawnerToBeSpawned = customerSpawnersUnspawned[randomIndex];
            customerSpawnerToBeSpawned.InitialSpawn();
            customerSpawnersUnspawned.Remove(customerSpawnerToBeSpawned);
        }
    }
}
