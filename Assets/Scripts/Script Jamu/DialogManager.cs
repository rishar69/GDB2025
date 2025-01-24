using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;

public class DialogManager : MonoBehaviour
{
    public TextAsset inkJSONAsset;
    private Story story;

    public PanelJamuAnimation UIPanel;
    public Text speakerText;
    public Text dialogText;
    public Button continueButton;
    public AnimationManager animationManager;

    public GameObject jamuMenuPanel;

    [Header("Typing Effect Settings")]
    public float typingSpeed = 0.05f;
    private bool isTyping = false;
    private string currentFullText = "";
    private Coroutine typingCoroutine;
    private bool isPaused = false;
    private string requestedRecipe;

    [SerializeField]
    private JamuBrewingGameManager jamuBrewingGameManager;

    void Start()
    {
        story = new Story(inkJSONAsset.text);
        continueButton.onClick.AddListener(HandleContinue);
        jamuMenuPanel.SetActive(false); // Sembunyikan menu jamu saat awal

        if (animationManager == null)
        {
            animationManager = GetComponent<AnimationManager>();
        }

        ContinueStory();
    }

    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && !isTyping && !isPaused)
        {
            HandleContinue();
        }
    }

    void HandleContinue()
    {
        if (isTyping)
        {
            CompleteTyping();
        }
        else
        {
            ContinueStory();
        }
    }

    void CompleteTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        dialogText.text = currentFullText;
        isTyping = false;
    }

    void ContinueStory()
    {
        if (isPaused) return;

        if (story.canContinue)
        {
            string text = story.Continue();
            currentFullText = text;
            ParseTags(story.currentTags);

            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeText(text));
        }
        else
        {
            continueButton.gameObject.SetActive(false);
        }
    }

    public void PauseStory() => isPaused = true;

    public void ResumeStory()
    {
        isPaused = false;
        ContinueStory();
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogText.text = "";

        foreach (char letter in text)
        {
            dialogText.text += letter;

            if (letter != ' ')
            {
                AudioManager.instance.PlaySFX("Typing");
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void ParseTags(List<string> tags)
    {
        foreach (string tag in tags)
        {
            switch (tag)
            {
           
                case var s when s.StartsWith("sfx:"):
                    string sfxName = s.Split(':')[1].Trim();
                    AudioManager.instance.PlaySFX(sfxName); 
                    break;
                case var s when s.StartsWith("speaker:"):
                    speakerText.text = s.Split(':')[1].Trim();
                    break;
                case var s when s.StartsWith("animation:"):
                    string animationName = s.Split(':')[1].Trim();
                    animationManager?.PlayAnimation(animationName);
                    break;
                case "ShowMenuJamu":
                    UIPanel.ShowJamuMenu();
                    PauseStory();
                    break;
                case var s when s.StartsWith("recipeRequest:"):
                    requestedRecipe = s.Split(':')[1].Trim();
                    break;
                case "fadeIn":
                    animationManager?.FadeIn();
                    break;
                case "fadeOut":
                    animationManager?.FadeOut();
                    break;
            }
        }
    }

    public void CheckRecipeOutcome()
    {
        bool isCorrect = jamuBrewingGameManager.CheckRecipeMatch(requestedRecipe);
        story.variablesState["recipe_correct"] = isCorrect;
        story.variablesState["recipe_wrong"] = !isCorrect;
        ResumeStory();
    }
}
