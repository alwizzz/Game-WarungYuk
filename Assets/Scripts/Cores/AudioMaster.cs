using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMaster : MonoBehaviour
{
    //[SerializeField] AudioClip clickSFX;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixer audioMixer;


    [SerializeField] private float masterVolumeValue;
    [SerializeField] private float musicVolumeValue;
    [SerializeField] private float sfxVolumeValue;

    private void Awake()
    {
        LoadData();
    }

    private void Start()
    {
        audioMixer.SetFloat("masterVolumeValue", masterVolumeValue);
        audioMixer.SetFloat("musicVolumeValue", musicVolumeValue);
        audioMixer.SetFloat("sfxVolumeValue", sfxVolumeValue);
    }

    private void LoadData()
    {
        masterVolumeValue = PlayerPrefs.GetFloat("masterVolumeValue", 0f);
        musicVolumeValue = PlayerPrefs.GetFloat("musicVolumeValue", 0f);
        sfxVolumeValue = PlayerPrefs.GetFloat("sfxVolumeValue", 0f);

        audioMixer.SetFloat("masterVolumeValue", masterVolumeValue);
        audioMixer.SetFloat("musicVolumeValue", musicVolumeValue);
        audioMixer.SetFloat("sfxVolumeValue", sfxVolumeValue);
    }

    public float GetMasterVolumeValue() => masterVolumeValue;
    public float GetMusicVolumeValue() => musicVolumeValue;
    public float GetSfxVolumeValue() => sfxVolumeValue;

    public void SetMasterVolumeValue(float value) { masterVolumeValue = value; }
    public void SetMusicVolumeValue(float value){ musicVolumeValue = value; }
    public void SetSfxVolumeValue(float value){ sfxVolumeValue = value; }



    public void PlayClickSFX()
    {
        //AudioSource.PlayClipAtPoint(
        //    clickSFX, 
        //    FindObjectOfType<MainCamera>().transform.position, 
        //    0.2f
        //);
        print("click");

        audioSource.Play();
    }

}
