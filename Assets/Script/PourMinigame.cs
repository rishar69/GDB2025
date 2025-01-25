using TMPro;
using UnityEngine;

public class PourMinigame : MonoBehaviour
{
    public RectTransform movingBar;
    public RectTransform targetZone;
    public TMP_Text feedbackText;

    public float barSpeed = 300f; // Kecepatan gerak bar
    private bool movingRight = true;

    void Update()
    {
        MoveBar();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckAccuracy();
        }
    }

    void MoveBar()
    {
        float step = barSpeed * Time.deltaTime;
        if (movingRight)
        {
            movingBar.anchoredPosition += new Vector2(step, 0);
            if (movingBar.anchoredPosition.x > 300) // Batas kanan
                movingRight = false;
        }
        else
        {
            movingBar.anchoredPosition -= new Vector2(step, 0);
            if (movingBar.anchoredPosition.x < -300) // Batas kiri
                movingRight = true;
        }
    }

    void CheckAccuracy()
    {
        float barPosition = movingBar.anchoredPosition.x;
        float targetStart = targetZone.anchoredPosition.x - (targetZone.sizeDelta.x / 2);
        float targetEnd = targetZone.anchoredPosition.x + (targetZone.sizeDelta.x / 2);

        if (barPosition >= targetStart && barPosition <= targetEnd)
        {
            feedbackText.text = "Bagus!";
            feedbackText.color = Color.green;
        }
        else
        {
            feedbackText.text = "Jelek!";
            feedbackText.color = Color.red;
        }

        // Reset feedback setelah beberapa detik
        Invoke("ClearFeedback", 1.5f);
    }

    void ClearFeedback()
    {
        feedbackText.text = "";
    }
}