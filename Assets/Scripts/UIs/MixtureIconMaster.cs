using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixtureIconMaster : MonoBehaviour
{
    [SerializeField] GameObject singleIcons;
    [SerializeField] GameObject doubleIcons;

    [SerializeField] Transform singleIconPivot;
    [SerializeField] Transform leftIconPivot;
    [SerializeField] Transform rightIconPivot;

    [SerializeField] List<MixtureIcon> icons;
    //Quaternion iconRotation;

    private void Awake()
    {
        //iconRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
    }

    public void AddIcon(MixtureIcon iconPrefab) 
    { 
        if(icons.Count == 2) { Debug.Log("error: attempts to add icon more than 2"); return; }

        icons.Add(iconPrefab);
        var i = icons.Count;
        if (i == 1)
        {
            MixtureIcon spawn1 = Instantiate(
                iconPrefab,
                transform.position,
                Quaternion.identity
            );
            spawn1.MoveToPivot(singleIconPivot);

            MixtureIcon spawn2 = Instantiate(
                iconPrefab,
                transform.position,
                Quaternion.identity
            );
            spawn2.MoveToPivot(leftIconPivot);
        }
        else if (i == 2)
        {
            MixtureIcon spawn = Instantiate(
                iconPrefab,
                transform.position,
                Quaternion.identity
            );
            spawn.MoveToPivot(rightIconPivot);
        }

        UpdateShownIcon();
    }
    public void ClearIcon()
    {
        icons.Clear();
        foreach (Transform child in singleIconPivot) { Destroy(child.gameObject); }
        foreach (Transform child in leftIconPivot) { Destroy(child.gameObject); }
        foreach (Transform child in rightIconPivot) { Destroy(child.gameObject); }

        UpdateShownIcon();
    }

    void UpdateShownIcon()
    {
        var i = icons.Count;
        if( i == 0)
        {
            singleIcons.SetActive(false);
            doubleIcons.SetActive(false);
        }
        else if (i == 1)
        {
            singleIcons.SetActive(true);
            doubleIcons.SetActive(false);
        }
        else if (i == 2)
        {
            singleIcons.SetActive(false);
            doubleIcons.SetActive(true);
        }

    }

}
