using UnityEngine;
using DG.Tweening;

public class AnimationManager : MonoBehaviour
{
    public Animator characterAnimator;

    private SpriteRenderer characterSpriteRenderer;

    void Awake()
    {
        // Ambil komponen SpriteRenderer saat dibutuhkan
        characterSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayAnimation(string animationName)
    {
        if (characterAnimator == null) return;

        ResetAllTriggers();
        characterAnimator.SetTrigger(animationName);
    }

    public void FadeIn(float duration = 0.5f)
    {
        if (characterSpriteRenderer == null)
        {
            characterSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        characterSpriteRenderer?.DOFade(1, duration); // Fade in
    }

    public void FadeOut(float duration = 0.5f)
    {
        if (characterSpriteRenderer == null)
        {
            characterSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        characterSpriteRenderer?.DOFade(0, duration); 
    }

    private void ResetAllTriggers()
    {
        foreach (AnimatorControllerParameter parameter in characterAnimator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
            {
                characterAnimator.ResetTrigger(parameter.name);
            }
        }
    }
}
