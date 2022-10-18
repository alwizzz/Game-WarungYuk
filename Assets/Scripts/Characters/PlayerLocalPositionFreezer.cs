using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocalPositionFreezer : MonoBehaviour
{
    
    void Update()
    {
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }
}
