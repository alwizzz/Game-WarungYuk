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
    }

    void Move()
    {

        float xValue = Input.GetAxis("Horizontal");
        float zValue = Input.GetAxis("Vertical");

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


        transform.Translate(actualMovementVector);

        if(movementVector != Vector3.zero)
        {
            var toRotation = Quaternion.LookRotation(movementVector, Vector3.up);
            playerBody.transform.rotation = Quaternion.RotateTowards(
                playerBody.transform.rotation,
                toRotation,
                rotateSpeed * Time.deltaTime
            );
        }

        UpdateIsMoving(movementVector);

    }

    public void SetBlockedForward(bool value) { blockedForward = value; }
    public void SetBlockedBackward(bool value) { blockedBackward = value; }
    public void SetBlockedLeft(bool value) { blockedLeft = value; }
    public void SetBlockedRight(bool value) { blockedRight = value; }
    public void SetIsAbleToMove(bool value) { isAbleToMove = value; }

    void UpdateIsMoving(Vector3 movementVector)
    {

        isMoving = (movementVector == Vector3.zero ? false : true);
        playerAnimator.SetBool("isMoving", isMoving);
    }


}