using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] GameObject playerBody;
    //[SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerInteraction playerInteraction;

    [SerializeField] bool isHolding;
    [SerializeField] Item heldItem;
    [SerializeField] Transform holdingPivot;

    Animator playerAnimator;

    private void Awake()
    {
        //playerMovement = GetComponent<PlayerMovement>();
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
        playerAnimator = playerBody.GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        UpdateIsHolding();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInteraction.HasInteractedObject())
        {
            playerInteraction.GetInteractedObject().Interact(this);
        }
    }

    public bool IsHolding() => isHolding;
    public Item GetHeldItem()
    {
        var temp = heldItem;
        heldItem = null;
        UpdateIsHolding();

        return temp;
    }

    public string GetHeldItemTypeString() => heldItem.GetType().ToString();
    public void SetHeldItem(Item input) { 
        heldItem = input;
        heldItem.MoveToPivot(holdingPivot);
        UpdateIsHolding();
    }
    void UpdateIsHolding() 
    { 
        isHolding = (heldItem ? true : false);
        playerAnimator.SetBool("isHolding", isHolding);
    }
}
