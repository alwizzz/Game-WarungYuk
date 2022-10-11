using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] float rotateSpeed = 0.1f;

    [SerializeField] Transform bodyTransform;
    MeshRenderer meshRenderer;
    [SerializeField] Color bumpMaterialColor;
    Color defaultMaterialColor;


    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        defaultMaterialColor = meshRenderer.material.color;
    }


    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            var addedVector = Vector3.forward;
            bodyTransform.eulerAngles = new Vector3(0, 0, 0);

            if (Input.GetKey(KeyCode.A))
            {
                addedVector += Vector3.left;
                bodyTransform.eulerAngles = new Vector3(0, -45, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                addedVector += Vector3.right;
                bodyTransform.eulerAngles = new Vector3(0, 45, 0);
            }

            transform.Translate(addedVector * moveSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            var addedVector = Vector3.left;
            bodyTransform.eulerAngles = new Vector3(0, -90, 0);

            if (Input.GetKey(KeyCode.W))
            {
                addedVector += Vector3.forward;
                bodyTransform.eulerAngles = new Vector3(0, -45, 0);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                addedVector += Vector3.back;
                bodyTransform.eulerAngles = new Vector3(0, -135, 0);
            }

            transform.Translate(addedVector * moveSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            var addedVector = Vector3.back;
            bodyTransform.eulerAngles = new Vector3(0, 180, 0);

            if (Input.GetKey(KeyCode.A))
            {
                addedVector += Vector3.left;
                bodyTransform.eulerAngles = new Vector3(0, -135, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                addedVector += Vector3.right;
                bodyTransform.eulerAngles = new Vector3(0, 135, 0);
            }

            transform.Translate(addedVector * moveSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            var addedVector = Vector3.right;
            bodyTransform.eulerAngles = new Vector3(0, 90, 0);

            if (Input.GetKey(KeyCode.W))
            {
                addedVector += Vector3.forward;
                bodyTransform.eulerAngles = new Vector3(0, 45, 0);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                addedVector += Vector3.back;
                bodyTransform.eulerAngles = new Vector3(0, 135, 0);
            }

            transform.Translate(addedVector * moveSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        meshRenderer.material.color = bumpMaterialColor;

        Debug.Log("enter");
    }

    private void OnCollisionExit(Collision collision)
    {
        meshRenderer.material.color = defaultMaterialColor;

        Debug.Log("exit");
    }
}
