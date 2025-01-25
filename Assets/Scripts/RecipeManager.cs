using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public List<RecipeSO> availableRecipes;

    public bool CheckAvailableRecipes(List<string> inputRecipe)
    {
        foreach (var recipe in availableRecipes)
        {
            if(IsRecipeCorrect(recipe.ingredients, inputRecipe, false))
            {
                return true;
            }
        }
        return false;
    }


    public bool IsRecipeCorrect(List<string> correctRecipe, List<string> inputRecipe, bool checkOrder)
    {
        if (correctRecipe == null || inputRecipe == null)
        {
            Debug.LogWarning("Correct recipe atau input recipe null!");
            return false;
        }

        if (correctRecipe.Count != inputRecipe.Count)
        {
            Debug.Log("Jumlah bahan tidak sesuai.");
            return false;
        }

        if (checkOrder)
        {
            // Periksa urutan bahan
            for (int i = 0; i < correctRecipe.Count; i++)
            {
                if (!correctRecipe[i].Equals(inputRecipe[i]))
                {
                    Debug.Log("Urutan bahan tidak sesuai.");
                    return false;
                }
            }
        }
        else
        {
            // Periksa tanpa memperhatikan urutan
            if (correctRecipe.Except(inputRecipe).Any() || inputRecipe.Except(correctRecipe).Any())
            {
                Debug.Log("Bahan tidak sesuai tanpa memperhatikan urutan.");
                return false;
            }
        }

        Debug.Log("Resep cocok.");
        return true;
    }
}
