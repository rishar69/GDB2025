using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class AnimationManager : MonoBehaviour
{
    public Animator characterAnimator;
    public SpriteRenderer spriteRenderer;


    public void PlayAnimation(string anim, string currentChar)
    {
        if (currentChar == "Gadis Baru")
        {
            currentChar = "Stephani";
        }

        if (characterAnimator != null)
        {
            string kata = currentChar + "_" + anim;
            Debug.Log("Play Anim: " + kata);


            characterAnimator.Play(kata);
        }
    }

    public void FadeIn(float duration = 1f)
    {
        spriteRenderer.gameObject.SetActive(true);
        StartCoroutine(FadeCoroutine(0f, 1f, duration));
    }


    public void FadeOut(float duration = 1f)
    {
        StartCoroutine(FadeCoroutine(1f, 0f, duration, ()=>
        {
            spriteRenderer.gameObject.SetActive(false);
        }));
    }


    private IEnumerator FadeCoroutine(float startAlpha, float endAlpha, float duration, 
        Action onComplete = null)
    {
        float elapsedTime = 0f;

        Color startColor = spriteRenderer.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }


        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, endAlpha);


        onComplete?.Invoke();
    }
}
