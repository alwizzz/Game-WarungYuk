using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixtureIcon : MonoBehaviour
{
    Quaternion neutralRotation;
    private void Awake()
    {
        neutralRotation = Quaternion.Euler(new Vector3(70f, 0f, 0f));
    }
    public void MoveToPivot(Transform pivotObject)
    {
        transform.parent = pivotObject;
        transform.position = pivotObject.position;
    }

    private void Start()
    {
        LockRotation();
    }
    private void Update()
    {
        LockRotation();
    }

    void LockRotation()
    {
        transform.rotation = neutralRotation;
    }
}
