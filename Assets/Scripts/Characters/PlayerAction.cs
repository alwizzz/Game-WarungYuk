using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] GameObject playerBody;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerInteraction playerInteraction;
    [SerializeField] Animator playerAnimator;

    [SerializeField] bool isHolding;
    [SerializeField] bool isUsingProcessor;

    [SerializeField] Item heldItem;
    [SerializeField] Transform holdingPivot;
    [SerializeField] GameMaster gameMaster;

    private void Start()
    {
        gameMaster = FindObjectOfType<GameMaster>();
        UpdateIsHolding();
    }

    private void Update()
    {
        var interactedObject = playerInteraction.GetInteractedObject();
        if (interactedObject != null && !gameMaster.IsPaused())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                interactedObject.Interact(this);
            } 

            if(interactedObject.GetType().ToString() == "Processor")
            {
                // if (Input.GetKeyDown(KeyCode.Q) && !isUsingProcessor ) 
                // {
                //     //Debug.Log("Q pressed"); 
                //     ((Processor)interactedObject).Process(this);
                // }
                // if (Input.GetKeyUp(KeyCode.Q) && isUsingProcessor) 
                // {
                //     ((Processor)interactedObject).AbortProcess(this);
                // }
                if (Input.GetKeyDown(KeyCode.LeftShift)) 
                {
                    if(isUsingProcessor){ ((Processor)interactedObject).AbortProcess(this); }
                    else {((Processor)interactedObject).Process(this);}
                }
            }
        }
    }

    public bool IsHolding() => isHolding;
    public Item GetHeldItem() => heldItem;
    public Item TakeHeldItem()
    {
        var temp = heldItem;
        heldItem = null;
        UpdateIsHolding();

        return temp;
    }
    public void GiveItemToHold(Item input) { 
        heldItem = input;
        heldItem.MoveToPivot(holdingPivot);
        UpdateIsHolding();
    }
    void UpdateIsHolding() 
    { 
        isHolding = (heldItem ? true : false);
        playerAnimator.SetBool("isHolding", isHolding);
    }

    public void HideHeldItem() { heldItem.gameObject.SetActive(false); }
    public void ShowHeldItem() { heldItem.gameObject.SetActive(true); }
    public void SetIsUsingProcessor(bool value) 
    { 
        isUsingProcessor = value;
        playerMovement.SetIsAbleToMove(!value);
    }
    public PlayerMovement GetPlayerMovement() => playerMovement;

}
