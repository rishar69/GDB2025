using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BobaPopiping : MonoBehaviour
{
    public GameObject circlePrefab;  // Assign a circle prefab in the inspector
    public TMP_Text countText;       // Assign a TMP_Text element for displaying count
    public TMP_Text messageText;     // Assign a TMP_Text element for "Passed" or "Failed" message
    public TMP_Text timerText;       // Assign a TMP_Text element for the timer
    public string nextSceneName;     // Name of the next scene to load if the player wins
    public Transform[] spawnPoints; // Array of spawn points for the circles
    public AudioClip clickSound;     // Assign the sound to play on every click

    private AudioSource audioSource; // AudioSource for playing sound effects
    private int circleCount = 0;     // Counter for circles spawned
    private const int targetCount = 50; // Target count to reach
    private float timer = 60f;       // Timer in seconds
    private bool isGameOver = false; // Flag to check if the game is over

    void Start()
    {
        // Ensure the message text is initially empty
        messageText.text = "";
        UpdateCountText();
        UpdateTimerText();

        // Get or add the AudioSource component
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (isGameOver) return;

        // Decrease the timer
        timer -= Time.deltaTime;

        // Ensure the timer doesn't go below zero
        if (timer < 0f) timer = 0f;

        UpdateTimerText();

        // Check if the timer has reached zero
        if (timer <= 0f && !isGameOver)
        {
            EndGame(false); // Game over with failure
        }

        // Check for mouse click to spawn circles
        if (Input.GetMouseButtonDown(0))
        {
            SpawnCircle();
        }
    }

    void SpawnCircle()
    {
        if (circleCount < targetCount && !isGameOver)
        {
            // Randomly select a spawn point
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Vector3 spawnPosition = spawnPoints[spawnIndex].position;

            // Instantiate the circle prefab at the selected spawn point
            Instantiate(circlePrefab, spawnPosition, Quaternion.identity);

            // Play the click sound
            PlayClickSound();

            // Increment the counter and update the UI
            circleCount++;
            UpdateCountText();

            // Check if the target count is reached
            if (circleCount >= targetCount)
            {
                EndGame(true); // Game over with success
            }
        }
    }

    void UpdateCountText()
    {
        countText.text = "Bubbles: " + circleCount;
    }

    void UpdateTimerText()
    {
        // Format timer to display whole seconds from 60 to 0
        timerText.text = "Time Left: " + Mathf.Ceil(timer).ToString() + "s";
    }

    void EndGame(bool isSuccess)
    {
        isGameOver = true;
        messageText.text = isSuccess ? "Passed!" : "Failed!";
        Invoke(isSuccess ? nameof(LoadNextScene) : nameof(RestartScene), 2f); // Wait 2 seconds
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void LoadNextScene()
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

    void PlayClickSound()
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}