using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererMaster : MonoBehaviour
{
    [SerializeField] bool hasChildren;
    //[SerializeField] List<Renderer> childrenRenderer;
    //[SerializeField] List<List<Material>> 
    [SerializeField] List<ChildRenderer> childRenderers;


    [System.Serializable]
    public class ChildRenderer
    {
        Renderer thisRenderer;
        [SerializeField] List<Material> materials;
        //[SerializeField] List<Color> defaultMaterialColors = new List<Color>();

        public ChildRenderer(Renderer renderer)
        {
            thisRenderer = renderer;
            materials = new List<Material>(renderer.materials);
            //foreach(Material m in materials){
            //    defaultMaterialColors.Add(m.color);
            //}
        }

        public Renderer GetRenderer() => thisRenderer;
        public List<Material> GetMaterials() => materials;
        //public List<Color> GetDefaultMaterialColors() => defaultMaterialColors;
    }

    void Awake()
    {
        hasChildren = transform.childCount > 0 ? true : false;
        if (hasChildren)
        {
            foreach(Renderer r in transform.GetComponentsInChildren<Renderer>()){
                childRenderers.Add(new ChildRenderer(r));
            }
        }
        else
        {
            childRenderers.Add(new ChildRenderer(GetComponent<Renderer>()));
        }
    }

    public List<ChildRenderer> GetChildRenderers() => childRenderers;
    public bool HasChildren() => hasChildren;
}
