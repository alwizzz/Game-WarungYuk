using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerInteraction : MonoBehaviour
{
    [SerializeField] CustomerMovement customerMovement;
    [SerializeField] CustomerAction customerAction;
    Transform grandparent;
    [SerializeField] bool hasInteracted;

    private void Start()
    {
        grandparent = transform.parent.parent;
        customerMovement = grandparent.GetComponent<CustomerMovement>();
        customerAction = grandparent.GetComponent<CustomerAction>();
        
        hasInteracted = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        CustomerTable customerTable = other.transform.GetComponent<CustomerTable>();
        if(customerTable != null && !hasInteracted)
        {
            hasInteracted = true;
            customerTable.Order(customerAction);
            customerMovement.ArrivedAtTable();
        } 
    }

    public Transform GetCustomer() => grandparent;
}
