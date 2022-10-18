using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererMaster : MonoBehaviour
{
    [SerializeField] bool hasChildren;
    [SerializeField] List<Renderer> childrenRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        hasChildren = transform.childCount > 0 ? true : false;
    }

    public List<Renderer> GetRenderers()
    {
        Debug.Log("welos");
        if (hasChildren)
        {
            Debug.Log(GetComponentsInChildren<Renderer>());
            childrenRenderer = new List<Renderer>(transform.GetComponentsInChildren<Renderer>());
            return childrenRenderer;
        }
        else
        {
            childrenRenderer.Add(GetComponent<Renderer>());
            return childrenRenderer;
        }
    }
}
