using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAction : MonoBehaviour
{
    [SerializeField] CustomerMovement customerMovement;
    Animator customerAnimator;
    [SerializeField] bool isHolding;
    [SerializeField] CompletedDish orderedDish;

    [SerializeField] private ParticleSystem correctOrderedDish;
    [SerializeField] private ParticleSystem wrongOrderedDish;


    [SerializeField] CompletedDish heldItem;
    [SerializeField] Transform holdingPivot;

    LevelMaster levelMaster;

    private void Awake()
    {
        levelMaster = FindObjectOfType<LevelMaster>();
    }

    private void Start()
    {
        customerAnimator = customerMovement.GetCustomerAnimator();
    }


    public void TakeOrderedDishFromTable(CompletedDish givenDish)
    {
        if (givenDish.GetCodeName() == orderedDish.GetCodeName())
        {
            // Debug.Log("given dish is correct");
            levelMaster.IncreasePoint(orderedDish.GetPoint(), customerMovement.IsAngry());
            FindObjectOfType<LevelSFXManager>().PlayCustomerSatisfiedSFX();

            correctOrderedDish.Play();
        }
        else
        {
            // Debug.Log("given dish is incorrect");
            levelMaster.DecreasePoint(orderedDish.GetPoint());
            FindObjectOfType<LevelSFXManager>().PlayCustomerDisappointedSFX();

            wrongOrderedDish.Play();
        }

        heldItem = givenDish;
        heldItem.MoveToPivot(holdingPivot);
        UpdateIsHolding();

        customerMovement.FinishedOrdering();
    }


    public CompletedDish GetOrderedDish() => orderedDish;

    void UpdateIsHolding() 
    { 
        isHolding = (heldItem ? true : false);
        customerAnimator.SetBool("isHolding", isHolding);
    
    }

    public CustomerMovement GetCustomerMovement() => customerMovement;
    public void SetOrderedDish(CompletedDish cDish) { orderedDish = cDish; }
}
