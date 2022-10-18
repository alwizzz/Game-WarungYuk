using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    [SerializeField] GameObject customerBody;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] Vector3 moveInsideVector;
    [SerializeField] Vector3 moveOutsideVector;

    [SerializeField] bool isMoving;
    Animator customerAnimator;
    [SerializeField] CustomerSpawner customerSpawner;

    public enum OrderState { GoInside, Wait, GoOutside}
    [SerializeField] OrderState currentOrderState;

    IEnumerator AngryProgressHandler;
    [SerializeField] float angryCounter;
    [SerializeField] float toBeAngryDuration;
    [SerializeField] bool isAngry;

    private void Start()
    {
        customerAnimator = customerBody.GetComponentInChildren<Animator>();
        moveInsideVector *= moveSpeed * Time.fixedDeltaTime; 
        moveOutsideVector = new Vector3(0f, 0f, 1f * moveSpeed * Time.fixedDeltaTime);

        currentOrderState = OrderState.GoInside;

        AngryProgressHandler = AngryProgress();
    }

    IEnumerator AngryProgress()
    {
        while(angryCounter > 0)
        {
            yield return null;
            angryCounter -= Time.deltaTime;
        }
        isAngry = true;
    }

    private void FixedUpdate()
    {
        if(currentOrderState == OrderState.GoInside)
        {
            Move(moveInsideVector);
        } 
        else if (currentOrderState == OrderState.GoOutside)
        {
            Move(moveOutsideVector);
        }
        UpdateIsMoving();
    }

    void Move(Vector3 moveVector)
    {
        transform.Translate(moveVector);

        var toRotation = Quaternion.LookRotation(moveVector, Vector3.up);
        customerBody.transform.rotation = Quaternion.RotateTowards(
            customerBody.transform.rotation,
            toRotation,
            rotateSpeed * Time.fixedDeltaTime
        );
    }

    public void ArrivedAtTable()
    {
        currentOrderState = OrderState.Wait;
        customerSpawner.SetHasArrived(true);

        StartCoroutine(AngryProgressHandler);
    }

    public void FinishedOrdering()
    {
        StopCoroutine(AngryProgressHandler);
        currentOrderState = OrderState.GoOutside;
    }

    void UpdateIsMoving()
    {
        isMoving = (currentOrderState == OrderState.Wait ? false : true);
        customerAnimator.SetBool("isMoving", isMoving);
    }

    public void SetCustomerSpawner(CustomerSpawner cs) { customerSpawner = cs; }
    public Animator GetCustomerAnimator() => customerAnimator;
    public void SetAngryConfig(float duration)
    {
        toBeAngryDuration = duration;
        isAngry = false;
        angryCounter = toBeAngryDuration;
    }

    public bool IsAngry() => isAngry;
}
