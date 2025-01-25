using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmptyCup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiText;
    [SerializeField] private Button resetButton;
    [SerializeField] private NamedSpriteVariant[] spriteVariants;

    private Dictionary<string, string> categorizedToppings = new Dictionary<string, string>();
    private Dictionary<string, SpriteRenderer> categoryRenderers = new Dictionary<string, SpriteRenderer>();
    private Dictionary<string, int> toppingVariantIndices = new Dictionary<string, int>();
    private Sprite[] originalSprites;

    [System.Serializable]
    public class NamedSpriteVariant
    {
        public string toppingName;
        public Sprite[] sprites;
    }

    private void Start()
    {
        InitializeComponents();
        SetupResetButton();
    }

    private void InitializeComponents()
    {
        InitializeCategories();
        InitializeVariantTracking();
        StoreOriginalSprites();
    }

    private void InitializeCategories()
    {
        foreach (Transform child in transform)
        {
            SpriteRenderer renderer = child.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                string categoryName = child.name;
                categorizedToppings[categoryName] = null;
                categoryRenderers[categoryName] = renderer;
            }
        }
    }

    private void InitializeVariantTracking()
    {
        foreach (var variant in spriteVariants)
        {
            toppingVariantIndices[variant.toppingName] = 0;
        }
    }

    private void StoreOriginalSprites()
    {
        originalSprites = categoryRenderers.Values.Select(r => r.sprite).ToArray();
    }

    private void SetupResetButton()
    {
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetCategories);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Topping"))
        {
            Toppings toppingAttributes = other.GetComponent<Toppings>();
            if (toppingAttributes != null)
            {
                TryAddTopping(toppingAttributes);
            }
        }
    }

    public bool TryAddTopping(Toppings toppingAttributes)
    {
        if (toppingAttributes == null) return false;

        string category = toppingAttributes.Type.ToString();

        if (categorizedToppings[category] == null)
        {
            AddTopping(category, toppingAttributes.Name);
            UpdateCategorySprite(toppingAttributes);
            UpdateUIText();
            return true;
        }

        Debug.Log($"Cannot add topping. Category '{category}' is already filled.");
        return false;
    }

    private void AddTopping(string category, string name)
    {
        categorizedToppings[category] = name;
        Debug.Log($"Added topping '{name}' to category '{category}'.");
    }

    private void UpdateCategorySprite(Toppings toppingAttributes)
    {
        string category = toppingAttributes.Type.ToString();
        if (categoryRenderers.TryGetValue(category, out SpriteRenderer renderer))
        {
            NamedSpriteVariant variantsForTopping =
                spriteVariants.FirstOrDefault(v => v.toppingName == toppingAttributes.Name);

            if (variantsForTopping?.sprites.Length > 0)
            {
                int currentIndex = toppingVariantIndices[toppingAttributes.Name];
                renderer.sprite = variantsForTopping.sprites[currentIndex];

                toppingVariantIndices[toppingAttributes.Name] =
                    (currentIndex + 1) % variantsForTopping.sprites.Length;
            }
        }
    }

    private void UpdateUIText()
    {
        if (uiText == null) return;

        var textContent = new StringBuilder();
        foreach (var category in categorizedToppings)
        {
            textContent.AppendLine($"{category.Key}: {category.Value ?? "None"}");
        }

        uiText.text = textContent.ToString().TrimEnd();
    }

    public void ResetCategories()
    {
        ResetCategorizedToppings();
        ResetSprites();
        InitializeVariantTracking();
        UpdateUIText();
        Debug.Log("Categories reset.");
    }

    private void ResetCategorizedToppings()
    {
        foreach (var category in categorizedToppings.Keys.ToList())
        {
            categorizedToppings[category] = null;
        }
    }

    private void ResetSprites()
    {
        for (int i = 0; i < categoryRenderers.Values.Count; i++)
        {
            categoryRenderers.Values.ElementAt(i).sprite = originalSprites[i];
        }
    }

    public void ClearToppings()
    {
        foreach (var category in categorizedToppings.Keys)
        {
            categorizedToppings[category] = null;
        }
        UpdateUIText();
        Debug.Log("All toppings have been cleared.");
    }

    public void DumpAllToppings()
    {
        Debug.Log("Current Toppings:");
        foreach (var category in categorizedToppings)
        {
            Debug.Log($"Category: {category.Key}, Topping: {category.Value ?? "None"}");
        }
    }
}