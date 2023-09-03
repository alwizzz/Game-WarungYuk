using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSFXManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip boilingSFX;
    [SerializeField] AudioClip fryingSFX;
    [SerializeField] AudioClip grillingSFX;
    [SerializeField] AudioClip cuttingSFX;
    [SerializeField] AudioClip customerSatisfiedSFX;
    [SerializeField] AudioClip customerDisappointedSFX;
    [SerializeField] AudioClip pickupItemSFX;
    [SerializeField] AudioClip putdownItemSFX;
    [SerializeField] AudioClip takeItemFromSpawnerSFX;
    [SerializeField] AudioClip trashBinSFX;
    [SerializeField] AudioClip mixSFX;
    [SerializeField] AudioClip popSFX;
    [SerializeField] AudioClip powerUpSpawnSFX;
    [SerializeField] AudioClip powerUpDespawnSFX;
    [SerializeField] AudioClip powerUpTakenSFX;

    public void PlayProcessorSFX(string processName)
    {
        if(processName == "REBUS")
        {
            PlayBoilingSFX();
        }
        else if(processName == "GORENG")
        {
            PlayFryingSFX();
        }
        else if (processName == "BAKAR")
        {
            PlayGrillingSFX();
        }
        else if (processName == "POTONG")
        {
            PlayCuttingSFX();
        }
        else
        {
            Debug.Log("invalid processName for SFX");
        }
    }

    void PlayFryingSFX()
    {
        audioSource.clip = fryingSFX;
        audioSource.Play();
    }
    void PlayBoilingSFX()
    {
        audioSource.clip = boilingSFX;
        audioSource.Play();
    }
    void PlayGrillingSFX()
    {
        audioSource.clip = grillingSFX;
        audioSource.Play();
    }
    void PlayCuttingSFX()
    {
        audioSource.clip = cuttingSFX;
        audioSource.Play();
    }

    public void StopSFX()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }

    public void PlayCustomerSatisfiedSFX()
    {
        audioSource.PlayOneShot(customerSatisfiedSFX);
    }

    public void PlayCustomerDisappointedSFX()
    {
        audioSource.PlayOneShot(customerDisappointedSFX);
    }

    public void PlayPickupItemSFX()
    {
        audioSource.PlayOneShot(pickupItemSFX);
    }
    public void PlayPutdownItemSFX()
    {
        audioSource.PlayOneShot(putdownItemSFX);
    }
    public void PlayTakeItemFromSpawnerSFX()
    {
        audioSource.PlayOneShot(takeItemFromSpawnerSFX);
    }
    public void PlayTrashBinSFX()
    {
        audioSource.PlayOneShot(trashBinSFX);
    }
    public void PlayMixSFX()
    {
        audioSource.PlayOneShot(mixSFX);
    }

    public void PlayPopSFX()
    {
        audioSource.PlayOneShot(popSFX);
    }

    public void PlayPowerUpSpawnSFX()
    {
        audioSource.PlayOneShot(powerUpSpawnSFX);
    }
    public void PlayPowerUpDespawnSFX()
    {
        audioSource.PlayOneShot(powerUpDespawnSFX);
    }
    public void PlayPowerUpTakenSFX()
    {
        audioSource.PlayOneShot(powerUpTakenSFX);
    }
}
