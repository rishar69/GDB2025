using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class JamuBrewingGameManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private PanelJamuAnimation uiAnimator;
    [SerializeField] private List<IngredientButton> ingredientButtons;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button resetButton;

    [Header("Gameplay Settings")]
    [SerializeField] private int maxIngredientSelection = 3;
    [SerializeField] private List<JamuRecipe> availableRecipes;
    [SerializeField] private RecipeMatcher recipeMatcher;

    private List<string> selectedIngredients = new List<string>();
    private Dictionary<string, int> ingredientSelectionCount = new Dictionary<string, int>();
    private bool isBrewingInProgress = false;
    private JamuRecipe currentMatchedRecipe;

    [SerializeField]
    private DialogManager dialogManager;


    private void Awake()
    {
        ValidateReferences();
    }

    private void Start()
    {

        InitializeButtons();
        InitializeIngredientCounts();


    }

    private void InitializeIngredientCounts()
    {
        ingredientSelectionCount.Clear();
        foreach (var button in ingredientButtons)
        {
            if (button != null)
            {
                ingredientSelectionCount[button.IngredientName] = 0;
            }
        }
    }

    private void ValidateReferences()
    {
        if (uiAnimator == null)
            Debug.LogError($"{gameObject.name}: Missing UI Animator reference!");

        if (ingredientButtons == null || ingredientButtons.Count == 0)
            Debug.LogError($"{gameObject.name}: No ingredient buttons assigned!");

        if (recipeMatcher == null)
            Debug.LogError($"{gameObject.name}: Missing RecipeMatcher reference!");

        if (availableRecipes == null || availableRecipes.Count == 0)
            Debug.LogError($"{gameObject.name}: No recipes assigned!");

        if (confirmButton == null)
            Debug.LogError($"{gameObject.name}: Missing Confirm Button reference!");

        if (resetButton == null)
            Debug.LogError($"{gameObject.name}: Missing Reset Button reference!");
    }

    private void InitializeButtons()
    {
        foreach (var button in ingredientButtons)
        {
            if (button == null) continue;

            var buttonComponent = button.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.RemoveAllListeners();
                buttonComponent.onClick.AddListener(() => OnIngredientSelected(button));
            }
            else
            {
                Debug.LogError($"{button.name}: Missing Button component!");
            }
        }

        if (resetButton != null)
        {
            resetButton.onClick.RemoveAllListeners();
            resetButton.onClick.AddListener(ResetRecipe);
        }
    }

    private void SetupConfirmAndResetButtons(bool active)
    {
        if (confirmButton != null) confirmButton.gameObject.SetActive(active);
        if (resetButton != null) resetButton.gameObject.SetActive(active);
    }

    public void StartNewBrewing()
    {
        if (isBrewingInProgress)
        {
            Debug.Log("Brewing already in progress!");
            return;
        }

        if (selectedIngredients.Count != maxIngredientSelection)
        {
            Debug.Log("Please select all ingredients first!");
            return;
        }
        isBrewingInProgress = true;

        // Log ingredients before matching
        Debug.Log("Selected ingredients for matching:");
        foreach (var ingredient in selectedIngredients)
        {
            Debug.Log($"- {ingredient}");
        }

        // Cek resep sebelum memulai proses brewing
        currentMatchedRecipe = recipeMatcher.MatchRecipe(availableRecipes, selectedIngredients);

        if (currentMatchedRecipe != null)
        {
            Debug.Log($"Found matching recipe: {currentMatchedRecipe.jamuName}");
        }
        else
        {
            Debug.Log("No matching recipe found!");
        }

        StartCoroutine(ProcessBrewingSequence());
    }

    private void OnIngredientSelected(IngredientButton button)
    {
        if (isBrewingInProgress)
        {
            Debug.Log("Cannot select ingredients while brewing!");
            return;
        }

        if (selectedIngredients.Count >= maxIngredientSelection)
        {
            Debug.Log("Maximum ingredients already selected!");
            return;
        }

        selectedIngredients.Add(button.IngredientName);

        if (!ingredientSelectionCount.ContainsKey(button.IngredientName))
        {
            ingredientSelectionCount[button.IngredientName] = 0;
        }
        ingredientSelectionCount[button.IngredientName]++;

        button.SetSelected(true);

        Debug.Log($"Selected {button.IngredientName} (Count: {ingredientSelectionCount[button.IngredientName]})");
        Debug.Log($"Total ingredients selected: {selectedIngredients.Count}");


    }

    private void DisableUnselectedButtons()
    {
        foreach (var button in ingredientButtons)
        {
            if (button != null)
            {
                button.SetGrayedOut(true);
            }
        }
    }

    private void EnableAllButtons()
    {
        foreach (var button in ingredientButtons)
        {
            if (button != null)
            {
                button.SetGrayedOut(false);
                button.SetSelected(false);
            }
        }
    }

    public void ResetRecipe()
    {
        selectedIngredients.Clear();
        InitializeIngredientCounts();
        EnableAllButtons();

        Debug.Log("Recipe reset! Please select ingredients again.");
    }

    private IEnumerator ProcessBrewingSequence()
    {
        yield return StartCoroutine(uiAnimator.AnimateBrewingSequence());

    }
    public bool CheckRecipeMatch(string requestedRecipe)
    {
        if (currentMatchedRecipe != null)
        {
            return currentMatchedRecipe.jamuName.Equals(requestedRecipe, System.StringComparison.OrdinalIgnoreCase);
        }
        return false;
    }
    public void ServeJamu()
    {
        if (!isBrewingInProgress)
        {
            Debug.LogWarning("Cannot serve Jamu - brewing not in progress!");
            return;
        }

        if (currentMatchedRecipe != null)
        {
            Debug.Log($"Successfully brewed: {currentMatchedRecipe.jamuName}");
            OnSuccessfulBrewing(currentMatchedRecipe);
        }
        else
        {
            Debug.Log("No matching recipe found for these ingredients!");
            OnFailedBrewing();
        }

        StartCoroutine(FinishBrewing());
    }
    public void OnServeButtonClicked()
    {
        {
            ServeJamu();

            // Hanya panggil jika ada resep yang cocok
            if (currentMatchedRecipe != null)
            {
                OnSuccessfulBrewing(currentMatchedRecipe);
            }
            else
            {
                OnFailedBrewing();
            }

            StartCoroutine(FinishBrewing());
        }


    }
    private void OnSuccessfulBrewing(JamuRecipe recipe)
    {
        Debug.Log($"Successfully created: {recipe.jamuName}");
        if (dialogManager != null)
        {
            dialogManager.CheckRecipeOutcome();
        }
        else
        {
            Debug.LogError("DialogManager tidak ditemukan saat memanggil CheckRecipeOutcome di OnSuccessfulBrewing.");
        }
    }

    private void OnFailedBrewing()
    {
        Debug.Log("Failed to create a valid Jamu recipe");
        if (dialogManager != null)
        {
            dialogManager.CheckRecipeOutcome();
        }
        else
        {
            Debug.LogError("DialogManager tidak ditemukan saat memanggil CheckRecipeOutcome di OnFailedBrewing.");
        }
    }

    private IEnumerator FinishBrewing()
    {
        yield return StartCoroutine(uiAnimator.HideMenuJamu());
        ResetGame();
    }

    private void ResetGame()
    {
        selectedIngredients.Clear();
        InitializeIngredientCounts();
        currentMatchedRecipe = null;


        foreach (var button in ingredientButtons)
        {
            if (button != null)
            {
                button.SetSelected(false);
                button.SetGrayedOut(false);
            }
        }
        isBrewingInProgress = false;
    }

    public bool IsBrewingInProgress => isBrewingInProgress;
    public int CurrentIngredientCount => selectedIngredients.Count;
    public IReadOnlyList<string> SelectedIngredients => selectedIngredients.AsReadOnly();
    public JamuRecipe CurrentRecipe => currentMatchedRecipe;
}