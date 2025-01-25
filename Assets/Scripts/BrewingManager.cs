using Ink.Parsed;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrewingManager : MonoBehaviour
{
    public GameObject brewingPanel;
    public GameObject brewing;
    public GameObject dialogpanel;

    private List<string> selectedIngredients = new List<string>();

    private bool isBrewingInProgress = false;

    public EmptyCup emptyCup;

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
        brewing.SetActive(true);
        dialogpanel.SetActive(false);
        Debug.Log("Start Brewing");


        isBrewingInProgress = true;
    }

    public void Serve(bool hasil)
    {
        List<string> inputRecipe = new List<string>();
        foreach(string s in emptyCup.categorizedToppings.Values)
        {
            inputRecipe.Add(s);
        }
        bool result = GameManager.Instance.recipeManager.CheckAvailableRecipes(inputRecipe);

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
        brewing.SetActive(false);
    }
}
