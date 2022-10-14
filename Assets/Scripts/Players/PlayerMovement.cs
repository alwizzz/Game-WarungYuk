using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject playerBody;
    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] float rotateSpeed = 0.1f;

    [SerializeField] bool isMoving;
    [SerializeField] bool isAbleToMove;

    [SerializeField] bool blockedForward;
    [SerializeField] bool blockedBackward;
    [SerializeField] bool blockedLeft;
    [SerializeField] bool blockedRight;

    //Rigidbody playerRigidbody;
    Animator playerAnimator;

    private void Start()
    {
        blockedForward = false;
        blockedBackward = false;
        blockedLeft = false;
        blockedRight = false;

        isMoving = false;
        isAbleToMove = true;

        //playerRigidbody = playerBody.GetComponent<Rigidbody>();
        playerAnimator = playerBody.GetComponentInChildren<Animator>();


    }

    private void Update()
    {
        if (isAbleToMove)
        {
            Move();
        }
        //RigidbodyMove();


    }

    void Move()
    {
        // ACTUALS are movement values that affected by movement-stopping collider

        float xValue = Input.GetAxis("Horizontal");
        float zValue = Input.GetAxis("Vertical");

        // discrete move vector value
        //if(xValue != 0f) { xValue = (xValue > 0f ? 1f : -1f); }

        var actualXValue = xValue;
        var actualZValue = zValue;

        if (blockedForward && actualZValue > 0) { actualZValue = 0; }
        if (blockedBackward && actualZValue < 0) { actualZValue = 0; }
        if (blockedRight && actualXValue > 0) { actualXValue = 0; }
        if (blockedLeft && actualXValue < 0) { actualXValue = 0; }

        var movementVector = new Vector3(
            xValue * moveSpeed * Time.deltaTime,
            0f,
            zValue * moveSpeed * Time.deltaTime
        );

        var actualMovementVector = new Vector3(
            actualXValue * moveSpeed * Time.deltaTime,
            0f,
            actualZValue * moveSpeed * Time.deltaTime
        );


        //movementVector.Normalize();
        transform.Translate(actualMovementVector);

        if(movementVector != Vector3.zero)
        {
            //isMoving = true;
            //playerBody.transform.forward = movementVector;
            var toRotation = Quaternion.LookRotation(movementVector, Vector3.up);
            playerBody.transform.rotation = Quaternion.RotateTowards(
                playerBody.transform.rotation,
                toRotation,
                rotateSpeed * Time.deltaTime
            );
        }
        //else
        //{
        //    isMoving = false;
        //}
        //Debug.Log(movementVector);

        UpdateIsMoving(movementVector);

    }

    //void RigidbodyMove()
    //{
    //    float xValue = Input.GetAxis("Horizontal");
    //    float zValue = Input.GetAxis("Vertical");

    //    //if (blockedForward && zValue > 0) { zValue = 0; }
    //    //if (blockedBackward && zValue < 0) { zValue = 0; }
    //    //if (blockedRight && xValue > 0) { xValue = 0; }
    //    //if (blockedLeft && xValue < 0) { xValue = 0; }

    //    var movementVector = new Vector3(
    //        xValue * moveSpeed * Time.deltaTime,
    //        0f,
    //        zValue * moveSpeed * Time.deltaTime
    //    );

    //    if (xValue != 0 || zValue != 0)
    //    {
    //        Debug.Log(
    //            "xValue: " + xValue +
    //            "; zValue: " + zValue +
    //            "; Time.deltaTime: " + Time.deltaTime +
    //            "; movementVector: " + movementVector
    //        );
    //    }
    //    playerRigidbody.velocity = movementVector;
    //}

    public void SetBlockedForward(bool value) { blockedForward = value; }
    public void SetBlockedBackward(bool value) { blockedBackward = value; }
    public void SetBlockedLeft(bool value) { blockedLeft = value; }
    public void SetBlockedRight(bool value) { blockedRight = value; }
    public void SetIsAbleToMove(bool value) { isAbleToMove = value; }

    void UpdateIsMoving(Vector3 movementVector)
    {
        //var roundedXValue = Mathf.RoundToInt(movementVector.x);
        //var roundedZValue = Mathf.RoundToInt(movementVector.z);

        //if(roundedXValue < 1 && roundedZValue < 1)
        //{
        //    isMoving = false;
        //} else
        //{
        //    isMoving = true;
        //}

        isMoving = (movementVector == Vector3.zero ? false : true);
        playerAnimator.SetBool("isMoving", isMoving);
    }


}
