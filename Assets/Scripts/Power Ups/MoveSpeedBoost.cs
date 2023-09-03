using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedBoost : PowerUp
{
    [SerializeField] private float boostedMoveSpeed;

    protected override void PowerUpPlayer()
    {
        PowerUpPlayer_Base();

        StartCoroutine(BoostMoveSpeed());
    }

    private IEnumerator BoostMoveSpeed()
    {
        FindObjectOfType<PlayerMovement>().SetMoveSpeed(boostedMoveSpeed);

        durationCounter = duration;
        while(durationCounter > 0f)
        {
            yield return null;
            durationCounter -= Time.deltaTime;
        }

        FindObjectOfType<PlayerMovement>().ResetMoveSpeed();
    }

    private void Update()
    {
        if (!pickedUp) RotatePowerUp();
    }

}
