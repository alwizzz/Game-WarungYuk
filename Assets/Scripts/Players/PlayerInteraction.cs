using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] int touchedInteractableCounter;
    [SerializeField] bool isMultiTouching;

    [SerializeField] Interactable primaryInteractedObject;
    [SerializeField] Interactable secondaryInteractedObject;
    [SerializeField] Interactable tertiaryInteractedObject; // yg ada di pojok, cuman disimpen tpi aslinya uninteractable
    [SerializeField] float distanceToPrimaryInteractedObject;
    [SerializeField] float distanceToSecondaryInteractedObject;
    [SerializeField] float distanceToTertiaryInteractedObject;


    private void Start()
    {
        touchedInteractableCounter = 0;
        UpdateIsMultiTouching();
        ResetDistances();
    }

    private void Update()
    {
        if (isMultiTouching)
        {
            DecideInteractedObjectPriority();
        }

        //if (Input.GetKeyDown(KeyCode.Space) && primaryInteractedObject != null)
        //{
        //    Debug.Log("Player interacts with " + primaryInteractedObject.name);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("touches Interactable");
        var theInteractable = other.gameObject.transform.GetComponent<Interactable>();
        touchedInteractableCounter++;
        UpdateIsMultiTouching();

        if (!isMultiTouching)
        {
            primaryInteractedObject = theInteractable;
        } else
        {
            if(touchedInteractableCounter == 2) { secondaryInteractedObject = theInteractable; }
            else if(touchedInteractableCounter == 3) { tertiaryInteractedObject = theInteractable; }

        }
        //theInteractable.TouchedByPlayer();
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("leaves Interactable");
        var theInteractable = other.gameObject.transform.GetComponent<Interactable>();
        touchedInteractableCounter--;
        UpdateIsMultiTouching();

        //theInteractable.LeftByPlayer();
        if(theInteractable == primaryInteractedObject)
        {
            primaryInteractedObject = secondaryInteractedObject;
            secondaryInteractedObject = (tertiaryInteractedObject ? tertiaryInteractedObject : null);
            tertiaryInteractedObject = null;
        } else if(theInteractable == secondaryInteractedObject)
        {
            secondaryInteractedObject = (tertiaryInteractedObject ? tertiaryInteractedObject : null);
            tertiaryInteractedObject = null;
        } else if(theInteractable == tertiaryInteractedObject)
        {
            tertiaryInteractedObject = null;
        }

    }

    void UpdateIsMultiTouching()
    {
        if(touchedInteractableCounter > 1)
        {
            isMultiTouching = true;
        } else
        {
            isMultiTouching = false;
            ResetDistances();
        }
    }

    void DecideInteractedObjectPriority()
    {
        distanceToPrimaryInteractedObject = Vector3.Distance(transform.position, primaryInteractedObject.transform.position);
        distanceToSecondaryInteractedObject = Vector3.Distance(transform.position, secondaryInteractedObject.transform.position);
        distanceToTertiaryInteractedObject = (
            tertiaryInteractedObject ?
                Vector3.Distance(transform.position, tertiaryInteractedObject.transform.position)
                :
                0
        );

        if (distanceToTertiaryInteractedObject < distanceToSecondaryInteractedObject && distanceToTertiaryInteractedObject != 0f)
        {
            SwapInteractedObjects(ref secondaryInteractedObject, ref tertiaryInteractedObject);
        }

        if (distanceToSecondaryInteractedObject < distanceToPrimaryInteractedObject && distanceToSecondaryInteractedObject != 0f)
        {
            //Debug.Log("before swap: " + primaryInteractedObject + " " + secondaryInteractedObject);
            SwapInteractedObjects(ref primaryInteractedObject, ref secondaryInteractedObject);
            //Debug.Log("after swap: " + primaryInteractedObject + " " + secondaryInteractedObject);

        }
    }

    void SwapInteractedObjects(ref Interactable first, ref Interactable second)
    {
        //Debug.Log("before swap: " +  first + " " + second);

        var temp = first;
        first = second;
        second = temp;

        //Debug.Log("after swap: " + first + " " + second);
    }
    
    void ResetDistances()
    {
        distanceToPrimaryInteractedObject = 0;
        distanceToSecondaryInteractedObject = 0;
        distanceToTertiaryInteractedObject = 0;
    }

    public bool HasInteractedObject() { return primaryInteractedObject; }
    public Interactable GetInteractedObject() { return primaryInteractedObject; }
}
