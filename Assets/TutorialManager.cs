using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private string currentState;
    [SerializeField] private List<TutorialModal> tutorialModals;
    private LevelMaster levelMaster;

    private void Awake()
    {
        levelMaster = FindObjectOfType<LevelMaster>();
    }


    // Start is called before the first frame update
    void Start()
    {

        currentState = "Hello";

        levelMaster.Pause_T();

        FindModal(currentState).Show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetState() => currentState;

    public void UpdateState(string state)
    {
        currentState = state;
    }

    private TutorialModal FindModal(string state)
    {
        var modal = tutorialModals.Find(x => x.GetState() == state);
        if(modal == null)
        {
            print("ERROR MODAL NOT FOUND");
            return null;
        } else
        {
            return modal;
        }
    }


    public void NextTutorialState(string state)
    {
        if(state != currentState)
        {
            return;
        }


        switch (state)
        {
            case "CookingRecipe":
                FindModal("FirstCustomer").Show();
                //currentState = "FirstCustomer";
                //FindModal("CookingRecipe").Hide();
                break;
            case "FirstCustomer":
                FindModal("MovePlate").Show();
                //currentState = "MovePlate";
                //FindModal("FirstCustomer").Hide();
                //levelMaster.Pause_T();
                break;
            case "DropPlate":
                FindModal("TakeDaging").Show();
                break;
            case "TakeDaging":
                FindModal("MixDaging").Show();
                break;
            case "MixDaging":
                FindModal("MixMie").Show();
                break;
            case "MixMie":
                FindModal("Cook").Show();
                break;
            case "Cook":
                FindModal("Deliver").Show();
                break;
            case "Deliver":
                FindModal("Finish").Show();
                //StartCoroutine(FinishTutorial());
                break;
            default:
                print("ERROR NEXT TUTORIAL STATE NOT FOUND");
                break;
        }

    }

    private IEnumerator FinishTutorial()
    {
        print("a");
        yield return new WaitForSeconds(4f);
        print("b");

        FindModal("Finish").Show();
    }
}
