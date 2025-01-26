using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StoryStep
{
    public string speaker; // Nama pembicara (opsional).
    [TextArea]
    public string text; // Teks cerita (opsional).
    public List<string> tags; // Tag untuk animasi, SFX, dll.
    public bool hasBranch; // Apakah langkah ini memiliki percabangan.
    public List<StoryBranch> branches; // Daftar percabangan (jika ada).
    public int nextStepIndex;
}

[System.Serializable]
public class StoryBranch
{
    public string condition; // Kondisi untuk memilih cabang (contoh: "recipe_correct", "recipe_wrong").
    public int nextStepIndex; // Index langkah cerita berikutnya.
}

[CreateAssetMenu(fileName = "NewStoryData", menuName = "Story/StoryData")]
public class StoryData : ScriptableObject
{
    public List<StoryStep> steps; // Semua langkah cerita.
}

public class StoryManager : MonoBehaviour
{

    [Header("UI Components")]
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogText;
    public Button continueButton;

    private StoryData storyData;
    private int currentStepIndex = 0;

    [Header("Typing Effect Settings")]
    public float typingSpeed = 0.005f;
    private bool isTyping = false;
    private string currentFullText = "";
    private Coroutine typingCoroutine;

    private bool isRecipeCorrect;

    private bool isPaused = false;

    private string currentChar;

    public List<string> correctRecipe = new List<string>();

    private void Awake()
    {
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(HandleContinue);
        isTyping = false;
        isPaused = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPaused)
        {
            HandleContinue();
        }
    }


    public void Init(StoryData story)
    {
        this.storyData = story;
        StartStory();
    }

    public void StartStory()
    {
        currentStepIndex = 0;
        isPaused = false;
        PlayStep();
    }

    private void HandleContinue()
    {
        if (isTyping)
        {
            CompleteTyping();
        }
        else
        {
            NextStep();
        }
    }
    private void CompleteTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        dialogText.text = currentFullText;
        isTyping = false;
        continueButton.gameObject.SetActive(true);
    }

    public void NextStep()
    {
        if (isPaused) return;

        if (storyData.steps[currentStepIndex].nextStepIndex >= storyData.steps.Count)
        {
            Debug.Log("Lebih dari range");
            return;
        }

        currentStepIndex = storyData.steps[currentStepIndex].nextStepIndex;

        if (currentStepIndex == 999)
        {
            Debug.Log("End Story");
            return;
        }

        PlayStep();
    }

    private void PlayStep()
    {
        if (currentStepIndex == 99)
        {
            Debug.Log("End Story");
            return;
        }
        var step = storyData.steps[currentStepIndex];

        // Tampilkan pembicara dan teks jika ada
        if (!string.IsNullOrEmpty(step.speaker))
        {
            GameManager.Instance.dialogPanel.SetActive(true);
            speakerText.text = step.speaker;
            currentChar = step.speaker;
        }

        if (!string.IsNullOrEmpty(step.text))
        {
            currentFullText = step.text;

            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(step.text));
        }

        ProcessTags(step.tags);

    }

    private void ProcessTags(List<string> tags)
    {
        if (tags == null || tags.Count == 0) return;

        foreach (var tag in tags)
        {
            if (tag.StartsWith("Audio: "))
            {
                string audioClipName = tag.Substring("Audio: ".Length);
                AudioManager.Instance.PlaySfx(audioClipName);
            }
            else if (tag.StartsWith("Animation: "))
            {
                string animationName = tag.Substring("Animation: ".Length);
                GameManager.Instance.animationManager.PlayAnimation(animationName, currentChar);
            }
            else if(tag.StartsWith("brewing"))
            {
                isPaused = true;
                GameManager.Instance.brewingManager.StartBrewing();
            }
            else if(tag.StartsWith("Char In: "))
            {
                string charName = tag.Substring("Char In: ".Length);
                GameManager.Instance.charManager.CharacterIn(charName);
            }
            else if(tag.StartsWith("Char Out: "))
            {
                string charName = tag.Substring("Char Out: ".Length);
                GameManager.Instance.charManager.CharacterOut(charName);
            }            
            else if(tag.StartsWith("NextDay"))
            {
                GameManager.Instance.NextDay();
            }
            else if(tag.StartsWith("RecipeRequest: "))
            {
                string recipe = tag.Substring("RecipeRequest: ".Length);
                correctRecipe = new List<string>(recipe.Split(','));
            }
            else
            {
                Debug.LogWarning("Unknown tag: " + tag + " at " + currentStepIndex);
            }
        }
    }

    private void ProcessBranch(StoryStep step)
    {
        foreach (var branch in step.branches)
        {
            if (EvaluateCondition(branch.condition))
            {
                currentStepIndex = branch.nextStepIndex;

                isPaused = false;
                PlayStep();
                return;
            }
        }
        NextStep();
    }

    private bool EvaluateCondition(string condition)
    {
        switch (condition)
        {
            case "recipe_correct":
                return isRecipeCorrect;
            case "recipe_wrong":
                return !isRecipeCorrect;
            default:
                return false;
        }
    }
    private IEnumerator TypeText(string text)
    {
        continueButton.gameObject.SetActive(false);
        isTyping = true;
        dialogText.text = "";

        foreach (char letter in text)
        {
            dialogText.text += letter;

            if (letter != ' ')
            {
                // AudioManager.Instance.PlaySfx("Typing");
            }

            yield return new WaitForSeconds(typingSpeed);
        }
        continueButton.gameObject.SetActive(true);
        isTyping = false;
    }

    public void PauseStory()
    {
        isPaused = true;
    }

    public void ResumeStory()
    {
        isPaused = false;
        NextStep();
    }

    public void BrewingResult(bool isCorrect)
    {
        isRecipeCorrect = isCorrect;
        var step = storyData.steps[currentStepIndex];
        if (step.hasBranch && step.branches != null)
        {
            ProcessBranch(step);
        }

    }

}
