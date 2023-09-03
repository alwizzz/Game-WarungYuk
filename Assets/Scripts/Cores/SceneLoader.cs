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
    [SerializeField] string codeHowToPlayScene;
    [SerializeField] string codePrologueScene;
    [SerializeField] string codeEpilogueScene;




    public void LoadMapScene()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();
        SceneManager.LoadScene(codeMapScene);
    }

    public void LoadMainMenuScene()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();
        SceneManager.LoadScene(codeMainMenuScene);
    }
    
    public void LoadHowToPlayScene()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();
        SceneManager.LoadScene(codeHowToPlayScene);
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();
        Application.Quit();
    }

    public void LoadLevelSumatraScene()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();
        SceneManager.LoadScene(codeLevelSumatraScene);
    }
    public void LoadAfterLevelSumatra()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();
        SceneManager.LoadScene(codeAfterLevelSumatra);
    }

    public void LoadLevelJawaScene()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();
        SceneManager.LoadScene(codeLevelJawaScene);
    }
    public void LoadAfterLevelJawa()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();
        SceneManager.LoadScene(codeAfterLevelJawa);
    }

    public void LoadLevelPapuaScene()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();
        SceneManager.LoadScene(codeLevelPapuaScene);
    }
    public void LoadAfterLevelPapua()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();
        SceneManager.LoadScene(codeAfterLevelPapua);
    }

    public void LoadLevelSulawesiScene()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();
        SceneManager.LoadScene(codeLevelSulawesiScene);
    }
    public void LoadAfterLevelSulawesi()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();
        SceneManager.LoadScene(codeAfterLevelSulawesi);
    }


    public void LoadLevelKalimantanScene()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();
        SceneManager.LoadScene(codeLevelKalimantanScene);
    }
    public void LoadAfterLevelKalimantan()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();
        SceneManager.LoadScene(codeAfterLevelKalimantan);
    }

    public void LoadAfterLevel(string levelName)
    {
        if (levelName == "Sumatra") { LoadAfterLevelSumatra(); }
        else if (levelName == "Jawa") { LoadAfterLevelJawa(); }
        else if (levelName == "Papua") { LoadAfterLevelPapua(); }
        else if (levelName == "Sulawesi") { LoadAfterLevelSulawesi(); }
        else if (levelName == "Kalimantan") { LoadAfterLevelKalimantan(); }
        else { Debug.Log("invalid levelName"); }
    }

    public void LoadPrologueScene()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();
        SceneManager.LoadScene(codePrologueScene);
    }

    public void LoadEpilogueScene()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();
        SceneManager.LoadScene(codeEpilogueScene);
    }

    public void LastLevelCompleted()
    {
        var afterLevel = FindObjectOfType<AfterLevel>();
        if (afterLevel != null && afterLevel.IsCompleted())
        {
            LoadEpilogueScene();
        } else
        {
            LoadMapScene();
        }
    }

}

