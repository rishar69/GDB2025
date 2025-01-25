using TMPro;
using UnityEngine;

public class AnimIdleTeks : MonoBehaviour
{
    public TMP_Text textComponent; // Assign your TextMeshPro text in the Inspector
    public float amplitude = 10f; // Distance the text will move up and down
    public float speed = 1f; // Speed of the movement

    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the text
        if (textComponent != null)
        {
            initialPosition = textComponent.rectTransform.anchoredPosition;
        }
        else
        {
            Debug.LogWarning("Text component not assigned.");
        }
    }

    void Update()
    {
        if (textComponent != null)
        {
            // Calculate the new Y position
            float offset = Mathf.Sin(Time.time * speed) * amplitude;
            textComponent.rectTransform.anchoredPosition = new Vector3(
                initialPosition.x,
                initialPosition.y + offset,
                initialPosition.z
            );
        }
    }
}