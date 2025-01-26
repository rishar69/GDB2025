using UnityEngine;
using UnityEngine.UI;

public class IdleAnimation : MonoBehaviour
{
    public RectTransform imageTransform; // Reference to the UI Image's RectTransform
    public float moveSpeed = 2f;         // Speed of the up-and-down movement
    public float moveRange = 30f;       // Range of movement in units

    private Vector3 initialPosition;    // To store the initial position of the image

    void Start()
    {
        if (imageTransform == null)
        {
            Debug.LogError("Image RectTransform is not assigned!");
            return;
        }

        // Save the initial position of the image
        initialPosition = imageTransform.anchoredPosition;
    }

    void Update()
    {
        if (imageTransform == null) return;

        // Calculate the new vertical position
        float newY = initialPosition.y + Mathf.Sin(Time.time * moveSpeed) * moveRange;

        // Apply the new position to the RectTransform
        imageTransform.anchoredPosition = new Vector2(initialPosition.x, newY);
    }
}