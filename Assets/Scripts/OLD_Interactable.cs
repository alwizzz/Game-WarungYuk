using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLD_Interactable : MonoBehaviour
{
    [SerializeField] Material touchedMaterial;
    Material defaultMaterial;

    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        defaultMaterial = meshRenderer.material;
    }

    // Update is called once per frame
    public void TouchedByPlayer()
    {
        meshRenderer.material = touchedMaterial;
    }
    public void LeftByPlayer()
    {
        meshRenderer.material = defaultMaterial;
    }
}
