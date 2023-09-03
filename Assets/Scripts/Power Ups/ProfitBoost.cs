using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfitBoost : PowerUp
{
    [SerializeField] private float profitBoostMultiplier;

    protected override void PowerUpPlayer()
    {
        PowerUpPlayer_Base();

        StartCoroutine(BoostProfit());
    }

    private IEnumerator BoostProfit()
    {
        FindObjectOfType<LevelMaster>().SetCorrectDishPointMultiplier(profitBoostMultiplier);

        durationCounter = duration;
        while (durationCounter > 0f)
        {
            yield return null;
            durationCounter -= Time.deltaTime;
        }

        FindObjectOfType<LevelMaster>().SetCorrectDishPointMultiplier(1f);
    }

    private void Update()
    {
        if (!pickedUp) RotatePowerUp();
    }
}
