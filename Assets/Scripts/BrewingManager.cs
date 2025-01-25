using Ink.Parsed;
using System.Collections.Generic;
using UnityEngine;

public class BrewingManager : MonoBehaviour
{
    public GameObject brewingPanel;

    private List<string> selectedIngredients = new List<string>();

    private bool isBrewingInProgress = false;


    private void AddIngredients(string ingredients)
    {
        selectedIngredients.Add(ingredients);
    }

    public void StartBrewing()
    {
        if (isBrewingInProgress)
        {
            return;
        }

        brewingPanel.SetActive(true);
        Debug.Log("Start Brewing");


        isBrewingInProgress = true;
    }

    public void Serve(bool hasil)
    {
        //bool result = GameManager.Instance.recipeManager.CheckAvailableRecipes(selectedIngredients);

        //if (result)
        //{

        //}
        //else
        //{

        //}

        var result = hasil;
        FinishBrewing(result);
    }

    private void FinishBrewing(bool result)
    {
        GameManager.Instance.storyManager.BrewingResult(result);
        Reset();
    }

    private void Reset()
    {

        brewingPanel.SetActive(false);
        selectedIngredients.Clear();
        isBrewingInProgress = false;
    }
}
