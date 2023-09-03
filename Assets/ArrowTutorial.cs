using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTutorial : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private float speed;

    [SerializeField] private GameObject model;
    [SerializeField] private Transform up;
    [SerializeField] private Transform down;

    [SerializeField] private bool movingDown;
    [SerializeField] private bool movingUp;


    private void Start()
    {
        model.transform.position = up.position;
        movingDown = true;
    }

    private IEnumerator MoveUp()
    {
        movingDown = false;
        yield return new WaitForSeconds(delay);
        movingUp = true;
    }

    private IEnumerator MoveDown()
    {
        movingUp = false;
        yield return new WaitForSeconds(delay);
        movingDown = true;
    }

    private void Update()
    {
        if(movingDown)
        {
            print("down");
            model.transform.position = Vector3.MoveTowards(model.transform.position, down.position, speed * Time.deltaTime);
            if(Mathf.Abs(Vector3.Distance(model.transform.position, down.position)) < 0.1f)
            {
                StartCoroutine(MoveUp());
            }
        } else if(movingUp)
        {
            print("up");

            model.transform.position = Vector3.MoveTowards(model.transform.position, up.position, speed * Time.deltaTime);
            if (Mathf.Abs(Vector3.Distance(model.transform.position, up.position)) < 0.1f)
            {
                StartCoroutine(MoveDown());
            }
        }
    }



}
