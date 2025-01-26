using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //[Header("Ink Assets")]
    //public List<TextAsset> inkJSONAsset;

    //[HideInInspector] public TextAsset currentInk;

    public List<StoryData> stories;


    [Header("UI Objects")]
    public GameObject blackPanel;
    public GameObject dayPanel;
    public GameObject dialogPanel;

    public TextMeshProUGUI dayText;

    [HideInInspector] public int currentDay;

    [Header("References")]
    public AnimationManager animationManager;
    public RecipeManager recipeManager;
    public BrewingManager brewingManager;
    public CharacterManager charManager;
    public StoryManager storyManager;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AudioManager.Instance.PlayBgm("BGM3");

        Debug.Log("Story Count: " + stories.Count);
        if (stories.Count <= 0)
        {
            stories.Add(Resources.Load<StoryData>("Day0"));
            Debug.Log("Add Story from resources");
        }

        StartDay(0);
    }

    public void NextDay()
    {
        currentDay++;
        if(stories.Count >= currentDay)
        {
            //end
            SceneManager.LoadScene("Menu");
        }

        StartDay(currentDay);
    }

    public void StartDay(int day)
    {
        currentDay = day;

        dayText.text = "Day " + day.ToString();
        DayTransition();
    }


    private void DayTransition()
    {
        blackPanel.SetActive(true);
        dayPanel.SetActive(true);
        dialogPanel.SetActive(false);
        

        StartCoroutine(FadeCoroutine(0f, 1f, 3f, dayPanel.GetComponent<CanvasGroup>(), () =>
        {
            blackPanel.SetActive(false);
            StartCoroutine(FadeCoroutine(1f, 0f, 2f, dayPanel.GetComponent<CanvasGroup>(), () =>
            {
                dayPanel.SetActive(false);
                storyManager.Init(stories[currentDay]);
            }));

        }));
    }


    private IEnumerator FadeCoroutine(float startAlpha, float endAlpha, float duration, 
        CanvasGroup cGroup, Action onComplete = null)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            cGroup.alpha = alpha;
            yield return null;
        }

        cGroup.alpha = endAlpha;
        onComplete?.Invoke();
    }
}
