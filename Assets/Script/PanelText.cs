using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelText : MonoBehaviour
{
    public List<string> textList;        // List of texts to be displayed
    public TextMeshProUGUI tmpText;      // Reference to the TextMeshPro component
    public Button nextButton;           // Assignable Button to go to the next text
    public string nextSceneName;         // Name of the scene to load if textList is empty
    private int currentTextIndex = 0;    // Index of the current text being displayed

    void Start()
    {
        if (tmpText == null)
        {
            Debug.LogError("TextMeshPro component is not assigned!");
            return;
        }

        if (nextButton == null)
        {
            Debug.LogError("Next button is not assigned!");
            return;
        }

        if (textList.Count > 0)
        {
            DisplayText();
        }
        else
        {
            Debug.LogWarning("No text in the text list! Changing scene...");
            ChangeScene();
            return;
        }

        // Add listener to the assigned button
        nextButton.onClick.AddListener(OnNextButtonClick);
    }

    // Function to display the current text in the TMP component
    void DisplayText()
    {
        tmpText.text = textList[currentTextIndex];
    }

    // Called when the assigned button is clicked
    void OnNextButtonClick()
{
    if (currentTextIndex < textList.Count - 1)
    {
        currentTextIndex++;
        DisplayText();
    }
    else
    {
        // Instead of disabling the GameObject, change to the next scene
        ChangeScene();
    }
}

    // Change to the specified next scene
    void ChangeScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name is not set!");
        }
    }
}