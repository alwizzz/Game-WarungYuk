using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string codeMainMenuScene;
    [SerializeField] string codeMapScene;

    [SerializeField] string codeLevelSumatraScene;
    [SerializeField] string codeAfterLevelSumatra;
    [SerializeField] string codeLevelJawaScene;
    [SerializeField] string codeAfterLevelJawa;
    [SerializeField] string codeLevelPapuaScene;
    [SerializeField] string codeAfterLevelPapua;
    [SerializeField] string codeLevelSulawesiScene;
    [SerializeField] string codeAfterLevelSulawesi;
    [SerializeField] string codeLevelKalimantanScene;
    [SerializeField] string codeAfterLevelKalimantan;


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

    public void LoadLevelSumatraScene()
    {
        SceneManager.LoadScene(codeLevelSumatraScene);
    }
    public void LoadAfterLevelSumatra()
    {
        SceneManager.LoadScene(codeAfterLevelSumatra);
    }

    public void LoadLevelJawaScene()
    {
        SceneManager.LoadScene(codeLevelJawaScene);
    }
    public void LoadAfterLevelJawa()
    {
        SceneManager.LoadScene(codeAfterLevelJawa);
    }

    public void LoadLevelPapuaScene()
    {
        SceneManager.LoadScene(codeLevelPapuaScene);
    }
    public void LoadAfterLevelPapua()
    {
        SceneManager.LoadScene(codeAfterLevelPapua);
    }

    public void LoadLevelSulawesiScene()
    {
        SceneManager.LoadScene(codeLevelSulawesiScene);
    }
    public void LoadAfterLevelSulawesi()
    {
        SceneManager.LoadScene(codeAfterLevelSulawesi);
    }


    public void LoadLevelKalimantanScene()
    {
        SceneManager.LoadScene(codeLevelKalimantanScene);
    }
    public void LoadAfterLevelKalimantan()
    {
        SceneManager.LoadScene(codeAfterLevelKalimantan);
    }

    public void LoadAfterLevel(string levelName)
    {
        if(levelName == "Sumatra") { LoadAfterLevelSumatra(); }
        else if (levelName == "Jawa") { LoadAfterLevelJawa(); }
        else if (levelName == "Papua") { LoadAfterLevelPapua(); }
        else if (levelName == "Sulawesi") { LoadAfterLevelSulawesi(); }
        else if (levelName == "Kalimantan") { LoadAfterLevelKalimantan(); }
        else { Debug.Log("invalid levelName"); }
    }

}

