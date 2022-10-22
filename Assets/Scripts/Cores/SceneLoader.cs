using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string codeMainMenuScene;
    [SerializeField] string codeMapScene;

    [SerializeField] string codeLevelSumatraScene;
    [SerializeField] string codeLevelJawaScene;
    [SerializeField] string codeLevelPapuaScene;
    [SerializeField] string codeLevelSulawesiScene;
    [SerializeField] string codeLevelKalimantanScene;



    public void LoadMapScene()
    {
        SceneManager.LoadScene(codeMapScene);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(codeMainMenuScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevelJawaScene()
    {
        SceneManager.LoadScene(codeLevelJawaScene);
    }
    public void LoadLevelSumatraScene()
    {
        SceneManager.LoadScene(codeLevelSumatraScene);
    }
    public void LoadLevelSulawesiScene()
    {
        SceneManager.LoadScene(codeLevelSulawesiScene);
    }
    public void LoadLevelPapuaScene()
    {
        SceneManager.LoadScene(codeLevelPapuaScene);
    }
    public void LoadLevelKalimantanScene()
    {
        SceneManager.LoadScene(codeLevelKalimantanScene);
    }
}
