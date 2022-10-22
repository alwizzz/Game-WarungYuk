using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNewGame : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    public void OnClick()
    {
        FindObjectOfType<GameMaster>().ResetAndLoadGame();
        sceneLoader.LoadMapScene();
    }
}
