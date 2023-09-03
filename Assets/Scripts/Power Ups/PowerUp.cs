using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] protected string powerName = "dummy";
    [SerializeField] protected bool pickedUp;
    [SerializeField] protected float duration;
    [SerializeField] protected float durationCounter;
    [SerializeField] protected float existDuration;
    [SerializeField] protected float existCounter;

    [SerializeField] protected float rotateSpeed;

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
        FindObjectOfType<LevelSFXManager>().PlayPowerUpSpawnSFX();

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
        if (pickedUp) { return; }

        print(gameObject + " has expired! respawning power up without offset");
        FindObjectOfType<PowerUpManager>().RespawnPowerUp(0f);

        //gameObject.SetActive(false);
        transform.position = FindObjectOfType<PowerUpManager>().transform.position;
        FindObjectOfType<LevelSFXManager>().PlayPowerUpDespawnSFX();
    }

    protected void OnTriggerEnter(Collider other)
    {
        pickedUp = true;
        PowerUpPlayer();
    }

    protected abstract void PowerUpPlayer();

    protected void PowerUpPlayer_Base()
    {
        print(gameObject + " has been taken by player! respawning power up with offset");
        FindObjectOfType<PowerUpManager>().RespawnPowerUp(duration);

        //gameObject.SetActive(false);
        transform.position = FindObjectOfType<PowerUpManager>().transform.position; //hide it;
        FindObjectOfType<LevelSFXManager>().PlayPowerUpTakenSFX();
    }

    protected void RotatePowerUp()
    {
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y + (rotateSpeed * Time.deltaTime) ,
            transform.eulerAngles.z
        ); 
    }
}
