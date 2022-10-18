using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] List<CustomerAction> customerPrefabs;
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
        yield return new WaitForSecondsRealtime(delay);
        var spawn = Instantiate(
            customerPrefabs[0],
            transform.position,
            Quaternion.identity
        );
        spawn.GetCustomerMovement().SetCustomerSpawner(this);
        
    }

    //public void Despawn(GameObject go)
    //{
    //    if (hasExistingSpawn)
    //    {
    //        Destroy(go);
    //        hasExistingSpawn = false;
    //    }
    //}

    public bool HasArrived() => hasArrived;
    public void SetHasArrived(bool value) { hasArrived = value; }
    public void SetHasExistingSpawn(bool value) { hasExistingSpawn = value; }
}
