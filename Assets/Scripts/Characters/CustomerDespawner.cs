using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDespawner : MonoBehaviour
{
    [SerializeField] CustomerSpawner customerSpawner;
    private void OnTriggerEnter(Collider other)
    {
        var customerInteraction = other.GetComponent<CustomerInteraction>();
        if( customerInteraction != null && customerSpawner.HasArrived())
        {
            Despawn(customerInteraction);
        }
    }

    void Despawn(CustomerInteraction customerInteraction)
    {
        var customerGO = customerInteraction.GetCustomer().gameObject;
        // Debug.Log(customerGO + " despawned");
        Destroy(customerGO);
        customerSpawner.SetHasExistingSpawn(false);
        customerSpawner.SetHasArrived(false);
    }
}
