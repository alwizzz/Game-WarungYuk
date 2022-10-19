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

    [SerializeField] Color color;


    private void Awake()
    {
        //playerMovement = GetComponent<PlayerMovement>();
        //playerInteraction = GetComponentInChildren<PlayerInteraction>();
        //playerAnimator = playerBody.GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        UpdateIsHolding();
    }

    private void Update()
    {
        var interactedObject = playerInteraction.GetInteractedObject();
        if (interactedObject)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                interactedObject.Interact(this);
            } 
            //else if(Input.GetKeyDown(KeyCode.Q) && interactedObject.GetType().ToString() == "Processor" && isHolding)
            //{
            //    Processor processor = (Processor)interactedObject;
            //    processor.Process(this);
            //}
            if(interactedObject.GetType().ToString() == "Processor")
            {
                if (Input.GetKeyDown(KeyCode.Q) && !isUsingProcessor) 
                {
                    //Debug.Log("Q pressed"); 
                    ((Processor)interactedObject).Process(this);
                }
                if (Input.GetKeyUp(KeyCode.Q) && isUsingProcessor) 
                {
                    ((Processor)interactedObject).AbortProcess(this);
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
