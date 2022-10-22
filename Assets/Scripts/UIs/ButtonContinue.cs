using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonContinue : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] Button button;

    private void Start()
    {
        if (FindObjectOfType<GameMaster>().HasGameData())
        {
            button.interactable = true;
        } 
        else
        {
            button.interactable = false;
        }
    }

    public void OnClick()
    {
        FindObjectOfType<GameMaster>().LoadGame();
        sceneLoader.LoadMapScene();
    }
}
