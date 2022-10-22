using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonContinue : MonoBehaviour
{
    [SerializeField] GameMaster gameMaster;
    [SerializeField] Button button;

    private void Start()
    {
        gameMaster = FindObjectOfType<GameMaster>();

        if (gameMaster.HasGameData())
        {
            button.interactable = true;
        } 
        else
        {
            button.interactable = false;
        }
    }
}
