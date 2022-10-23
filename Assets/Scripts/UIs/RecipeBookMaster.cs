using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBookMaster : MonoBehaviour
{
    [SerializeField] int currentIndex;
    [SerializeField] bool isOpened;
    [SerializeField] List<RecipeBook> recipeBooks;
    [SerializeField] Button previousButton;
    [SerializeField] Button nextButton;

    [SerializeField] Button recipeBookButton;

    void ResetIndex()
    {
        currentIndex = 0;
    }

    public void OpenRecipeBook()
    {
        ResetIndex();
        FindObjectOfType<AudioMaster>().PlayClickSFX();


        recipeBooks[currentIndex].gameObject.SetActive(true);
        isOpened = true;
        UpdateButtons();
    }

    public void CloseRecipeBook()
    {
        recipeBooks[currentIndex].gameObject.SetActive(false);
        isOpened = false;
        UpdateButtons();
        FindObjectOfType<AudioMaster>().PlayClickSFX();


        recipeBookButton.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void NextRecipeBook()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();

        recipeBooks[currentIndex].gameObject.SetActive(false);
        currentIndex++;
        recipeBooks[currentIndex].gameObject.SetActive(true);

        UpdateButtons();
    }

    public void PreviousRecipeBook()
    {
        FindObjectOfType<AudioMaster>().PlayClickSFX();

        recipeBooks[currentIndex].gameObject.SetActive(false);
        currentIndex--;
        recipeBooks[currentIndex].gameObject.SetActive(true);

        UpdateButtons();
    }

    void UpdateButtons()
    {
        if (currentIndex == 0)
        {
            previousButton.interactable = false;
            nextButton.interactable = true;
        }
        else if (currentIndex == recipeBooks.Count - 1)
        {
            previousButton.interactable = true;
            nextButton.interactable = false;
        }
        else
        {
            previousButton.interactable = true;
            nextButton.interactable = true;
        }
    }
}
