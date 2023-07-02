using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private GameObject holder;

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private float masterVolumeValue;
    [SerializeField] private Slider masterVolumeSlider;

    [SerializeField] private float musicVolumeValue;
    [SerializeField] private Slider musicVolumeSlider;

    [SerializeField] private float sfxVolumeValue;
    [SerializeField] private Slider sfxVolumeSlider;

    private AudioMaster audioMaster;

    private void Start()
    {
        audioMaster = FindObjectOfType<AudioMaster>();
        LoadData();
    }

    public void Show()
    {
        holder.SetActive(true);
        FindObjectOfType<AudioMaster>().PlayClickSFX();
    }

    public void Hide()
    {
        holder.SetActive(false);
        FindObjectOfType<AudioMaster>().PlayClickSFX();

    }


    private void LoadData()
    {
        masterVolumeValue = audioMaster.GetMasterVolumeValue();
        musicVolumeValue = audioMaster.GetMusicVolumeValue();
        sfxVolumeValue = audioMaster.GetSfxVolumeValue();

        masterVolumeSlider.value = masterVolumeValue;
        musicVolumeSlider.value = musicVolumeValue;
        sfxVolumeSlider.value = sfxVolumeValue;
    }

    public void ChangeMasterVolume()
    {
        masterVolumeValue = masterVolumeSlider.value;
        audioMaster.SetMasterVolumeValue(masterVolumeValue);

        PlayerPrefs.SetFloat("masterVolumeValue", masterVolumeValue);
        audioMixer.SetFloat("masterVolumeValue", masterVolumeValue);

        //FindObjectOfType<AudioMaster>().PlayClickSFX();
    }

    public void ChangeMusicVolume()
    {
        musicVolumeValue = musicVolumeSlider.value;
        audioMaster.SetMusicVolumeValue(musicVolumeValue);

        PlayerPrefs.SetFloat("musicVolumeValue", musicVolumeValue);
        audioMixer.SetFloat("musicVolumeValue", musicVolumeValue);

        //FindObjectOfType<AudioMaster>().PlayClickSFX();
    }

    public void ChangeSfxVolume()
    {
        sfxVolumeValue = sfxVolumeSlider.value;
        audioMaster.SetSfxVolumeValue(sfxVolumeValue);

        PlayerPrefs.SetFloat("sfxVolumeValue", sfxVolumeValue);
        audioMixer.SetFloat("sfxVolumeValue", sfxVolumeValue);

        //FindObjectOfType<AudioMaster>().PlayClickSFX();
    }
}
