using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] private string powerName = "dummy";
    [SerializeField] private float duration;
    [SerializeField] private float existDuration;
    [SerializeField] private float existCounter;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetPowerName() => powerName;

    public void SetupAndActivate(float existDuration)
    {
        this.existDuration = existDuration;

        StartCoroutine(ExistTimer());
    }

    protected IEnumerator ExistTimer()
    {
        existCounter = existDuration;
        while(existCounter > 0f)
        {
            yield return null;
            existCounter -= Time.deltaTime;
        }

        Expire();
    }

    protected void Expire()
    {
        print(gameObject + " has expired! respawning power up without offset");
        FindObjectOfType<PowerUpManager>().RespawnPowerUp(0f);

        gameObject.SetActive(false);
    }
}
