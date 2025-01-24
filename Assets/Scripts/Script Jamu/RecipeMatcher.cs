using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RecipeMatcher : MonoBehaviour
{
    public JamuRecipe MatchRecipe(List<JamuRecipe> recipes, List<string> selectedIngredients)
    {
        if (recipes == null || selectedIngredients == null)
        {
            Debug.LogWarning("RecipeMatcher: Received null parameters!");
            return null;
        }

        // Buat dictionary untuk menghitung frekuensi bahan yang dipilih
        Dictionary<string, int> selectedIngredientCounts = new Dictionary<string, int>();
        foreach (var ingredient in selectedIngredients)
        {
            if (selectedIngredientCounts.ContainsKey(ingredient))
                selectedIngredientCounts[ingredient]++;
            else
                selectedIngredientCounts[ingredient] = 1;
        }

        foreach (var recipe in recipes)
        {
            if (recipe == null || recipe.ingredients == null)
            {
                Debug.LogWarning("RecipeMatcher: Found null recipe or ingredients!");
                continue;
            }

            // Cek apakah jumlah bahan sesuai
            if (recipe.ingredients.Count != selectedIngredients.Count)
            {
                continue;
            }

            // Buat dictionary untuk menghitung frekuensi bahan dalam resep
            Dictionary<string, int> recipeIngredientCounts = new Dictionary<string, int>();
            foreach (var ingredient in recipe.ingredients)
            {
                if (recipeIngredientCounts.ContainsKey(ingredient))
                    recipeIngredientCounts[ingredient]++;
                else
                    recipeIngredientCounts[ingredient] = 1;
            }

            // Bandingkan frekuensi bahan yang dipilih dengan resep
            bool isMatch = true;
            foreach (var pair in recipeIngredientCounts)
            {
                string ingredient = pair.Key;
                int count = pair.Value;

                // Cek apakah bahan ada dan jumlahnya sama
                if (!selectedIngredientCounts.ContainsKey(ingredient) ||
                    selectedIngredientCounts[ingredient] != count)
                {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch)
            {
                Debug.Log($"Recipe matched: {recipe.jamuName} with ingredients: {string.Join(", ", selectedIngredients)}");
                return recipe;
            }
        }

        Debug.Log($"No matching recipe found for ingredients: {string.Join(", ", selectedIngredients)}");
        return null;
    }

    // Helper method untuk debugging
    public void LogIngredientCounts(Dictionary<string, int> counts)
    {
        foreach (var pair in counts)
        {
            Debug.Log($"Ingredient: {pair.Key}, Count: {pair.Value}");
        }
    }
}