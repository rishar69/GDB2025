//using UnityEngine;
//using UnityEngine.UI;
//using Ink.Runtime;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;



//public class DialogManager : MonoBehaviour
//{

//    [Header("UI Components")]
//    public TextMeshProUGUI speakerText;
//    public TextMeshProUGUI dialogText;
//    public Button continueButton;

//    private TextAsset inkJSONAsset;
//    private Story story;

//    [Header("Typing Effect Settings")]
//    public float typingSpeed = 0.05f;
//    private bool isTyping = false;
//    private string currentFullText = "";
//    private Coroutine typingCoroutine;


//    private bool isPaused = false;
//    [HideInInspector]public string requestedRecipe;

//    void Start()
//    {
//        continueButton.onClick.RemoveAllListeners();
//        continueButton.onClick.AddListener(HandleContinue);

//        //ContinueStory();
//        isPaused = true;
//    }

//    public void Init()
//    {
//        //inkJSONAsset = GameManager.Instance.currentInk;
//        story = new Story(inkJSONAsset.text);

//        isPaused = false;
//        ContinueStory();
//    }

//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0) && !isTyping && !isPaused)
//        {
//            HandleContinue();
//        }
//    }

//    private void HandleContinue()
//    {
//        if (isTyping)
//        {
//            CompleteTyping();
//        }
//        else
//        {
//            ContinueStory();
//        }
//    }

//    private void CompleteTyping()
//    {
//        if (typingCoroutine != null)
//        {
//            StopCoroutine(typingCoroutine);
//        }
//        dialogText.text = currentFullText;
//        isTyping = false;
//    }

//    private void ContinueStory()
//    {
//        if (isPaused) return;

//        if (story.canContinue)
//        {
//            string text = story.Continue();
//            currentFullText = text;
//            ParseTags(story.currentTags);


//            if (typingCoroutine != null)
//            {
//                StopCoroutine(typingCoroutine);
//            }
//            typingCoroutine = StartCoroutine(TypeText(text));
//        }
//        else
//        {
//            continueButton.gameObject.SetActive(false);
//            GameManager.Instance.NextDay();
//        }
//    }

//    public void ResumeStory()
//    {
//        isPaused = false;
//        ContinueStory();
//    }
//    public void PauseStory() => isPaused = true;

//    private IEnumerator TypeText(string text)
//    {
//        isTyping = true;
//        dialogText.text = "";

//        foreach (char letter in text)
//        {
//            dialogText.text += letter;

//            if (letter != ' ')
//            {
//                //AudioManager.Instance.PlaySfx("Typing");
//            }

//            yield return new WaitForSeconds(typingSpeed);
//        }

//        isTyping = false;
//    }

//    private void ParseTags(List<string> tags)
//    {
//        foreach (string tag in tags)
//        {
//            switch (tag)
//            {
//                case var s when s.StartsWith("sfx:"):
//                    string sfxName = s.Split(':')[1].Trim();
//                    //AudioManager.Instance.PlaySfx(sfxName);
//                    break;
//                case var s when s.StartsWith("speaker:"):
//                    speakerText.text = s.Split(':')[1].Trim();

//                    GameManager.Instance.charManager.SwitchCharacter(s.Split(':')[1].Trim());

//                    break;
//                case var s when s.StartsWith("animation:"):
//                    string animationName = s.Split(':')[1].Trim();
//                    GameManager.Instance.animationManager.PlayAnimation(animationName);
//                    break;
//                case "ShowBrewingPanel":
//                    //UIPanel.ShowJamuMenu();
//                    // start brewing
//                    PauseStory();
//                    break;
//                case var s when s.StartsWith("recipeRequest:"):
//                    requestedRecipe = s.Split(':')[1].Trim();
//                    break;
//                case "fadeIn":
//                    GameManager.Instance.animationManager.FadeIn();
//                    break;
//                case "fadeOut":
//                    GameManager.Instance.animationManager.FadeOut();
//                    break;
//            }
//        }
//    }

//    public void BrewingResult(bool isCorrect)
//    {
//        story.variablesState["recipe_correct"] = isCorrect;
//        story.variablesState["recipe_wrong"] = !isCorrect;
//        ResumeStory();
//    }


//}
