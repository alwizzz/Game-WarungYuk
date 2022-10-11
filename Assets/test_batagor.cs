using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_batagor : MonoBehaviour
{
    [SerializeField] Table table;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = table.GetPlacingPivot();
    }

    private void Update()
    {
        
    }


}
