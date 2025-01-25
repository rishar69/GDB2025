using TMPro;
using UnityEngine;

public class PourMinigame : MonoBehaviour
{
    public RectTransform movingBar;
    public RectTransform targetZone;
    public TMP_Text feedbackText;
    public TMP_Text counterText;

    public float barSpeed = 300f; // Kecepatan gerak bar
    private bool movingRight = true;

    private int successfulPresses = 0; // Berapa kali user benar
    private int totalPresses = 0; // Total percobaan yang dilakukan
    private int maxPresses = 3; // Jumlah maksimum percobaan

    void Start()
    {
        UpdateCounterText();
    }

    void Update()
    {
        if (totalPresses < maxPresses) // Hanya bergerak jika masih ada percobaan
        {
            MoveBar();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckAccuracy();
            }
        }
    }

    void MoveBar()
    {
        float step = barSpeed * Time.deltaTime;
        if (movingRight)
        {
            movingBar.anchoredPosition += new Vector2(step, 0);
            if (movingBar.anchoredPosition.x > 500) // Batas kanan
                movingRight = false;
        }
        else
        {
            movingBar.anchoredPosition -= new Vector2(step, 0);
            if (movingBar.anchoredPosition.x < -500) // Batas kiri
                movingRight = true;
        }
    }

    void CheckAccuracy()
    {
        totalPresses++; // Tambahkan jumlah percobaan

        float barPosition = movingBar.anchoredPosition.x;
        float targetStart = targetZone.anchoredPosition.x - (targetZone.sizeDelta.x / 2);
        float targetEnd = targetZone.anchoredPosition.x + (targetZone.sizeDelta.x / 2);

        if (barPosition >= targetStart && barPosition <= targetEnd)
        {
            successfulPresses++;
            feedbackText.text = "Bagus!";
            feedbackText.color = Color.green;
        }
        else
        {
            feedbackText.text = "Jelek!";
            feedbackText.color = Color.red;
        }

        UpdateCounterText();

        // Reset feedback setelah beberapa detik
        Invoke("ClearFeedback", 1.5f);

        if (totalPresses >= maxPresses)
        {
            EndMiniGame();
        }
    }

    void UpdateCounterText()
    {
        counterText.text = $"Benar: {successfulPresses}/{maxPresses}";
    }

    void ClearFeedback()
    {
        feedbackText.text = "";
    }

    void EndMiniGame()
    {
        feedbackText.text = "Selesai!";
        feedbackText.color = Color.yellow;
    }
}