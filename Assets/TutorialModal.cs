using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TutorialModal : MonoBehaviour
{
    [SerializeField] [TextArea(2,4)] private string state;
    [SerializeField] private GameObject holder;

    [SerializeField] private UnityEvent callbackOnStart;
    //[SerializeField] private UnityEvent callbackOnFinish;

    public string GetState() => state;
    public void Show()
    {
        //FindObjectOfType<TutorialManager>().UpdateState(state);

        holder.SetActive(true);
        callbackOnStart?.Invoke();
    }

    public void Hide()
    {
        holder.SetActive(false);
        //callbackOnFinish?.Invoke();
    }
}
