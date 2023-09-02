using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private List<PowerUp> powerUpPrefabs;

    [SerializeField] private bool hasExistingPowerUp;
    [SerializeField] private PowerUp lastPowerUp;
    
    [SerializeField] private List<FreeGround> freeGrounds;
    [SerializeField] private FreeGround lastFreeGround;

    [SerializeField] private float powerUpExistDuration;
    [SerializeField] private float spawnDelayMin;
    [SerializeField] private float spawnDelayMax;

    [SerializeField] private float spawnDelay;
    [SerializeField] private float spawnDelayCounter;

    


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SpawnDelayTimer());
        RespawnPowerUp(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnDelayTimer(float offset = 0f)
    {
        spawnDelay = Random.Range(spawnDelayMin, spawnDelayMax);
        spawnDelayCounter = spawnDelay + offset;

        while(spawnDelayCounter > 0f)
        {
            yield return null;
            spawnDelayCounter -= Time.deltaTime;
        }

        SpawnPowerUp();
    }

    private void SpawnPowerUp()
    {
        List<PowerUp> powerUpPrefabsCopy = new List<PowerUp>(powerUpPrefabs);
        if(lastPowerUp != null)
        {
            //powerUpPrefabsCopy.Remove(lastPowerUp);
            powerUpPrefabsCopy.RemoveAll(x => x.GetPowerName() == lastPowerUp.GetPowerName());

            powerUpPrefabsCopy.ForEach(x => print("--> " + x));

            Destroy(lastPowerUp.gameObject);
        }
        PowerUp randomPowerUpPrefab = powerUpPrefabsCopy[Random.Range(0, powerUpPrefabsCopy.Count - 1)];

        List<FreeGround> freeGroundsCopy = new List<FreeGround>(freeGrounds);
        if (lastFreeGround != null)
            freeGroundsCopy.Remove(lastFreeGround);
        FreeGround randomFreeGround = freeGroundsCopy[Random.Range(0, freeGroundsCopy.Count - 1)];
        lastFreeGround = randomFreeGround;

        print("=========== spawning power up: " + randomPowerUpPrefab.GetPowerName() + " on " + randomFreeGround);
        PowerUp randomPowerUp = Instantiate(
            randomPowerUpPrefab,
            randomFreeGround.transform.position + new Vector3(0, 1.5f, 0),
            Quaternion.identity
        );
        randomPowerUp.transform.parent = transform;
        randomPowerUp.SetupAndActivate(powerUpExistDuration);

        lastPowerUp = randomPowerUp;

        hasExistingPowerUp = true;
    }

    public void RespawnPowerUp(float offset)
    {
        hasExistingPowerUp = false;

        StartCoroutine(SpawnDelayTimer(offset));
    }
}
