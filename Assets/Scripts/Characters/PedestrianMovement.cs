using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianMovement : MonoBehaviour
{
    [SerializeField] private GameObject body;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    

    private Vector3 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        moveVector = Vector3.back * moveSpeed * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(moveVector);
        SyncRotation();
    }

    private void SyncRotation()
    {
        var toRotation = Quaternion.LookRotation(moveVector, Vector3.up);
        body.transform.rotation = Quaternion.RotateTowards(
            body.transform.rotation,
            toRotation,
            rotateSpeed * Time.fixedDeltaTime
        );
    }
}
