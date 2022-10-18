using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAction : MonoBehaviour
{
    [SerializeField] CustomerMovement customerMovement;
    [SerializeField] Animator customerAnimator;
    [SerializeField] bool isHolding;
    [SerializeField] CompletedDish orderedDish;

    [SerializeField] CompletedDish heldItem;
    [SerializeField] Transform holdingPivot;

    [SerializeField] float penaltyMultiplier = 0.25f;

    LevelMaster levelMaster;

    private void Awake()
    {
        levelMaster = FindObjectOfType<LevelMaster>();
    }


    public void TakeOrderedDishFromTable(CompletedDish givenDish)
    {
        if (givenDish.GetCodeName() == orderedDish.GetCodeName())
        {
            Debug.Log("given dish is correct");
            levelMaster.IncreasePoint(orderedDish.GetPoint());
        }
        else
        {
            Debug.Log("given dish is incorrect");
            float temp = (penaltyMultiplier * orderedDish.GetPoint());
            temp /= 100f;
            int penaltyPoint = Mathf.RoundToInt(temp) * 100;
            levelMaster.DecreasePoint(penaltyPoint);
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
}
