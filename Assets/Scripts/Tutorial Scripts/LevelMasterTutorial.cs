using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelMasterTutorial : MonoBehaviour
{
    [Header("Level State")]
    [SerializeField] protected bool isPaused;
    [SerializeField] protected bool gameHasStarted;

    [Header("Cooking Recipe")]
    [SerializeField] protected TextAsset jsonFile;
    protected CookingRecipe cookingRecipe;
    [SerializeField] protected List<ProcessedIngredient> processedIngredientPrefabs;
    [SerializeField] protected List<CompletedDish> completedDishPrefabs;
    [SerializeField] protected List<int> completedDishSpawnCounters;
    protected bool stillHasCounters;

    [Header("UI Cache")]
    [SerializeField] protected GameObject modalPause;
    [SerializeField] protected GameObject modalCookingGuide;


    private void Awake()
    {
        cookingRecipe = JsonUtility.FromJson<CookingRecipe>(jsonFile.ToString());
        gameHasStarted = false;
        isPaused = false;
        modalPause.SetActive(false);
    }

    private void Start()
    {
        UpdateStillHasCounters();
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //isPaused = !isPaused;
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }

    }
    public bool IsPaused() => isPaused;

    public CookingRecipe GetCookingRecipe() => cookingRecipe;

    public CompletedDish GetCompletedDishPrefab(string codeName)
    {
        var cDish = completedDishPrefabs.Find((cd) => cd.GetCodeName() == codeName);
        return cDish ? cDish : null;
    }
    public ProcessedIngredient GetProcessedIngredientPrefab(string codeName)
    {
        var pIng = processedIngredientPrefabs.Find((pIng) => pIng.GetCodeName() == codeName);
        return pIng ? pIng : null;
    }

    public List<CompletedDish> GetCompletedDishPrefabs() => completedDishPrefabs;

    private void UpdateStillHasCounters()
    {
        bool value = false;
        foreach (int x in completedDishSpawnCounters)
        {
            if (x > 0)
            {
                value = true;
                break;
            }
        }
        stillHasCounters = value;
    }

    public CompletedDish GetRandomCompletedDishPrefab()
    {
        int randomIndex = Random.Range(0, completedDishPrefabs.Count);
        if (stillHasCounters)
        {
            while (completedDishSpawnCounters[randomIndex] == 0)
            {
                randomIndex = Random.Range(0, completedDishPrefabs.Count);
            }

            completedDishSpawnCounters[randomIndex] -= 1;
            UpdateStillHasCounters();
        }

        return completedDishPrefabs[randomIndex];
    }

    public bool GameHasStarted() => gameHasStarted;

    public void EndLevel()
    {
        Debug.Log("LEVEL TIMER: TIME'S UP");

        //FindObjectOfType<SceneLoader>().LoadAfterLevel(cookingRecipe.region);
    }


    // ======================== UI

    public void Pause()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();

        if (!gameHasStarted) { return; }

        Time.timeScale = 0f;

        modalPause.SetActive(true);
        isPaused = true;

        CloseCookingGuide();
    }

    public void Unpause()
    {

        Time.timeScale = 1f;

        modalPause.SetActive(false);
        isPaused = false;
        FindObjectOfType<AudioMaster>().PlayClickSFX();
    }

    public void BackToMap()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();

        if (isPaused) { Time.timeScale = 1f; }
        FindObjectOfType<SceneLoader>().LoadMapScene();
    }

    public void OpenCookingGuide()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();

        modalCookingGuide.SetActive(true);
    }

    public void CloseCookingGuide()
    {

        FindObjectOfType<AudioMaster>().PlayClickSFX();

        modalCookingGuide.SetActive(false);
    }
}
