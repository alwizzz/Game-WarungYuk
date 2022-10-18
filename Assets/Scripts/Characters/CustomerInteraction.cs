using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerInteraction : MonoBehaviour
{
    [SerializeField] CustomerMovement customerMovement;
    [SerializeField] CustomerAction customerAction;
    Transform grandparent;

    private void Start()
    {
        grandparent = transform.parent.parent;
        customerMovement = grandparent.GetComponent<CustomerMovement>();
        customerAction = grandparent.GetComponent<CustomerAction>();

    }
    private void OnTriggerEnter(Collider other)
    {
        CustomerTable customerTable = other.transform.GetComponent<CustomerTable>();
        if(customerTable != null)
        {
            Debug.Log("woyyy");
            customerTable.Order(customerAction);
            customerMovement.ArrivedAtTable();
        } 
    }

    public Transform GetCustomer() => grandparent;
}
