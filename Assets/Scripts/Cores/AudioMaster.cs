using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    [SerializeField] AudioClip clickSFX;

    public void PlayClickSFX()
    {
        AudioSource.PlayClipAtPoint(
            clickSFX, 
            FindObjectOfType<MainCamera>().transform.position, 
            0.2f
        );
    }

}
