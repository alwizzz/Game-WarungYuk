using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTime : PowerUp
{
    [SerializeField] private int bonusTimeValue;

    protected override void PowerUpPlayer()
    {
        AddBonusTime();
    }

    private void AddBonusTime()
    {
        PowerUpPlayer_Base();

        FindObjectOfType<LevelTimer>().AddLevelDuration(bonusTimeValue);
    }

    private void Update()
    {
        if (!pickedUp) RotatePowerUp();
    }

}
