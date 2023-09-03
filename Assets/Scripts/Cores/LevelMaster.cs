using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelMaster : MonoBehaviour
{
    [Header("Enter Level Countdown")]
    [SerializeField] private float enterLevelDuration;
    [SerializeField] private float enterLevelCounter;
    [SerializeField] private Slider enterLevelSlider;

    [Header("Start Level Countdown")]
    [SerializeField] private float startLevelCountdown = 3f;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private float startLevelCountdownCounter;
    [SerializeField] private int startLevelCountdownCounterInt;


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

    [Header("Point Config")]
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private int totalPoints;
    [SerializeField] private int oneStarMinimumPoints;
    [SerializeField] private int twoStarMinimumPoints;
    [SerializeField] private int threeStarMinimumPoints;
    [SerializeField] private float wrongDishPenaltyMultiplier;
    [SerializeField] private float customerIsAngryPenaltyMultiplier;

    [SerializeField] private float correctDishPointMultiplier;

    [Header("Level Statistic")]
    [SerializeField] private int successfulOrders;
    [SerializeField] private int failedOrders;

    [Header("Spawn Config")]
    [SerializeField] private float initialSpawnDelayMin;
    [SerializeField] private float initialSpawnDelayMax;
    [SerializeField] private float spawnDelayMin;
    [SerializeField] private float spawnDelayMax;
    
    [Header("Duration Config")]
    [SerializeField] private LevelTimer levelTimer;
    [SerializeField] private float levelDuration;
    [SerializeField] private float toBeAngryDuration;

    [Header("UI Cache")]
    [SerializeField] protected GameObject modalPause;
    [SerializeField] protected GameObject modalCookingGuide;

    private GameMaster gm;


    private void Awake()
    {
        gm = FindObjectOfType<GameMaster>();

        if (gm.OnTutorial())
        {
            Awake_T();
        } else
        {
            Awake_N();
        }
    }

    private void Start()
    {
        correctDishPointMultiplier = 1f;

        if (gm.OnTutorial())
        {
            Start_T();
        }
        else
        {
            Start_N();
        }
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

    #region Normal Mechanic

    private void Awake_N()
    {
        cookingRecipe = JsonUtility.FromJson<CookingRecipe>(jsonFile.ToString());
        gameHasStarted = false;
        levelTimer.SetLevelDuration(levelDuration);
        isPaused = false;
        modalPause.SetActive(false);
    }

    private void Start_N()
    {
        UpdateStillHasCounters();
        UpdateDisplayPoint();

        successfulOrders = 0;
        failedOrders = 0;

        StartCoroutine(EnterLevelCountdown());
    }

    private IEnumerator EnterLevelCountdown()
    {
        print("33333333333333333333333333333333333333333 " + Time.timeScale); 
        enterLevelCounter = enterLevelDuration;
        enterLevelSlider.value = 1f;

        while(enterLevelCounter > 0f)
        {
            yield return null;
            enterLevelCounter -= Time.deltaTime;

            enterLevelSlider.value = enterLevelCounter / enterLevelDuration;
        }

        Destroy(enterLevelSlider.gameObject);
    }



    public void IncreasePoint(int dishPoint, bool customerIsAngry) 
    {
        successfulOrders++;

        if (customerIsAngry)
        {
            int penaltiedPoint = Mathf.RoundToInt(customerIsAngryPenaltyMultiplier * dishPoint * correctDishPointMultiplier);

            totalPoints += penaltiedPoint;
            Debug.Log("Increased point by " + penaltiedPoint.ToString("n0") + " with multiplier " + correctDishPointMultiplier);
        }
        else
        {
            int realDishPoint = Mathf.RoundToInt(dishPoint * correctDishPointMultiplier);

            totalPoints += realDishPoint;
            Debug.Log("Increased point by " + realDishPoint.ToString("n0") + " with multiplier " + correctDishPointMultiplier);
        }

        UpdateDisplayPoint();
    }
    public void DecreasePoint(int dishPoint)
    {
        failedOrders++;

        int penaltiedPoint = Mathf.RoundToInt(wrongDishPenaltyMultiplier * dishPoint);

        totalPoints -= penaltiedPoint;
        totalPoints = (totalPoints >= 0 ? totalPoints : 0);

        Debug.Log("Decreased point by " + penaltiedPoint.ToString("n0"));
        UpdateDisplayPoint();
    }

    private void UpdateDisplayPoint()
    {
        pointText.text = totalPoints.ToString();
    }

    public float GetInitialSpawnDelayMin() => initialSpawnDelayMin;
    public float GetInitialSpawnDelayMax() => initialSpawnDelayMax;
    public float GetSpawnDelayMin() => spawnDelayMin;
    public float GetSpawnDelayMax() => spawnDelayMax;
    public float GetToBeAngryDuration() => toBeAngryDuration;



    public CompletedDish GetRandomCompletedDishPrefab()
    {
        int randomIndex = Random.Range(0, completedDishPrefabs.Count);
        if (stillHasCounters)
        {
            while(completedDishSpawnCounters[randomIndex] == 0)
            {
                randomIndex = Random.Range(0, completedDishPrefabs.Count);
            } 

            completedDishSpawnCounters[randomIndex] -= 1;
            UpdateStillHasCounters();
        }

        return completedDishPrefabs[randomIndex];
    }

    public bool GameHasStarted() => gameHasStarted;

    private IEnumerator StartLevel()
    {
        // start level countdown stuffs
        countdownText.gameObject.SetActive(true);

        startLevelCountdownCounter = startLevelCountdown;
        startLevelCountdownCounterInt = (int)startLevelCountdownCounter;
        countdownText.text = startLevelCountdownCounterInt.ToString();
        while(startLevelCountdownCounter > 0f)
        {
            startLevelCountdownCounter -= Time.deltaTime;
            if((int)startLevelCountdownCounter < startLevelCountdownCounterInt - 1)
            {
                startLevelCountdownCounterInt--;
                countdownText.text = startLevelCountdownCounterInt.ToString();
            }

            yield return null;
        }

        countdownText.gameObject.SetActive(false);


        gameHasStarted = true;
        levelTimer.ContinueTimer();
        Debug.Log("LEVEL TIMER: GAME STARTED");
    }

    public void EndLevel()
    {
        Debug.Log("LEVEL TIMER: TIME'S UP");

        int obtainedStars = 0;
        if(totalPoints >= oneStarMinimumPoints) { obtainedStars = 1; }
        if(totalPoints >= twoStarMinimumPoints) { obtainedStars = 2; }
        if(totalPoints >= threeStarMinimumPoints) { obtainedStars = 3; }

        FindObjectOfType<GameMaster>().SetLevelProgressCache(successfulOrders, failedOrders, totalPoints, obtainedStars);
        FindObjectOfType<SceneLoader>().LoadAfterLevel(cookingRecipe.region);
    }


    // ======================== UI

    public void Pause()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();

        if (!gameHasStarted) { return; }

        Time.timeScale = 0f;
        isPaused = true;

        levelTimer.PauseTimer();

        modalPause.SetActive(true);
        
        CloseCookingGuide();
    }

    public void Unpause()
    {

        levelTimer.ContinueTimer();

        modalPause.SetActive(false);
        FindObjectOfType<AudioMaster>().PlayClickSFX();

        // tutorial inject
        if (FindObjectOfType<GameMaster>().OnTutorial())
        {
            if (!FindObjectOfType<TutorialManager>().OnModalOpen())
            {
                Time.timeScale = 1f;
                isPaused = false;
            }
        }
        else
        {
            Time.timeScale = 1f;
            isPaused = false;
        }


        
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
        // at enter level, cooking guide cant be closed until the timer runs out
        if (enterLevelCounter > 0f) { return; } 

        FindObjectOfType<AudioMaster>().PlayClickSFX();

        modalCookingGuide.SetActive(false);
        if (!gameHasStarted) { StartCoroutine(StartLevel()); }
    }

    #endregion


    #region Tutorial Mechanic

    private void Awake_T()
    {
        cookingRecipe = JsonUtility.FromJson<CookingRecipe>(jsonFile.ToString());
        gameHasStarted = false;
        isPaused = false;
        modalPause.SetActive(false);
    }

    private void Start_T()
    {
        UpdateStillHasCounters();
        StartLevel_T();
    }

    private void StartLevel_T()
    {
        gameHasStarted = true;
        Debug.Log("TUTORIAL: GAME STARTED");
    }

    public void Pause_T()
    {
        isPaused = true;
        Time.timeScale = 0f;

        FindObjectOfType<TutorialManager>().SetOnModalOpen(true);
    }

    public void Unpause_T()
    {
        isPaused = false;
        Time.timeScale = 1f;

        FindObjectOfType<TutorialManager>().SetOnModalOpen(false);

    }

    #endregion

    #region Shared between


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

    #endregion


    public void SetCorrectDishPointMultiplier(float value)
    {
        correctDishPointMultiplier = value;
    }



}
