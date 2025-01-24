using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BobaPopiping : MonoBehaviour
{
    public GameObject circlePrefab; // Assign a circle prefab in the inspector
    public TMP_Text countText;         // Assign a UI Text element for displaying count
    public TMP_Text messageText;       // Assign a UI Text element for "Passed" or "Failed" message
    public TMP_Text timerText;         // Assign a UI Text element for the timer
    public string nextSceneName;   // Name of the next scene to load if the player wins

    private int circleCount = 0;   // Counter for circles spawned
    private const int targetCount = 100; // Target count to reach
    private float timer = 30f;     // Timer in seconds
    private bool isGameOver = false; // Flag to check if the game is over

    void Start()
    {
        // Ensure the message text is initially empty
        messageText.text = "";
        UpdateCountText();
        UpdateTimerText();
    }

    void Update()
    {
        if (isGameOver) return;

        // Decrease the timer
        timer -= Time.deltaTime;
        UpdateTimerText();

        // Check if the timer has reached zero
        if (timer <= 0f)
        {
            timer = 0f;
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
            // Get mouse position and convert to world point
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0f; // Set Z to 0 for 2D

            // Instantiate the circle prefab at the calculated position
            Instantiate(circlePrefab, spawnPosition, Quaternion.identity);

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
        countText.text = "Count: " + circleCount;
    }

    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.Ceil(timer).ToString() + "s";
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
}