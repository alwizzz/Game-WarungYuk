using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Processor processor;
    [SerializeField] Transform progressPivot;
    float processDuration;

    private void Awake()
    {
        processor = transform.parent.GetComponent<Processor>();
        processor.SetProgressBar(this);
        processDuration = processor.GetProcessDuration();
    }

    private void FixedUpdate()
    {
        if (processor.IsProcessing())
        {
            float normalizedProgress = (processor.GetTimeCounter() / processDuration);
            progressPivot.localScale = new Vector3(normalizedProgress, 1f, 1f);
        }
    }

    public void Hide() { gameObject.SetActive(false); }
    public void Show() { gameObject.SetActive(true); }

}
