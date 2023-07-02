using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNewGame : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] GameObject confirmationModal;
    public void OnClick()
    {
        if (FindObjectOfType<GameMaster>().HasGameData())
        {
            confirmationModal.SetActive(true);
        }
        else
        {
            OverwriteAndNewGame();
        }
    }

    public void OverwriteAndNewGame()
    {
        FindObjectOfType<GameMaster>().ResetAndLoadGame();
        sceneLoader.LoadMapScene();
    }

    public void Cancel()
    {
        confirmationModal.SetActive(false);
    }
}
