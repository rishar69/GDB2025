using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class PanelJamuAnimation : MonoBehaviour
{
    [Header("Jamu Menu References")]
    [SerializeField] private RectTransform JamuMenu;

    [Header("Menu References")]
    [SerializeField] private RectTransform ingredientsMenu;
    [SerializeField] private RectTransform makeMenu;
    [SerializeField] private RectTransform cutsceneMenu;
    [SerializeField] private RectTransform serveMenu;

    [Header("Cutscene")]
    [SerializeField] private Image[] cutsceneImages;

    [Header("Animation Settings")]
    [SerializeField] private float slideDuration = 0.7f;
    [SerializeField] private float fadeDuration = 0.5f;

    [Header("Menu Positions")]
    [SerializeField] private Vector2 JamuMenuOnScreen = Vector2.zero;
    [SerializeField] private Vector2 JamuMenuOffScreen = new Vector2(0, -800);
    [SerializeField] private Vector2 makeMenuOnScreen = Vector2.zero;
    [SerializeField] private Vector2 makeMenuOffScreen = new Vector2(0, -800);
    [SerializeField] private Vector2 cutsceneOnScreen = Vector2.zero;
    [SerializeField] private Vector2 cutsceneOffScreen = new Vector2(-800, 0);
    [SerializeField] private Vector2 ingredientsOnScreen = Vector2.zero;
    [SerializeField] private Vector2 serveOnScreen = Vector2.zero;
    [SerializeField] private Vector2 serveOffScreen = new Vector2(0, 800);

    private bool isAnimating = false;
    private CanvasGroup ingredientsCanvasGroup;
    private CanvasGroup makeCanvasGroup;
    private CanvasGroup cutsceneCanvasGroup;
    private CanvasGroup serveCanvasGroup;
    private CanvasGroup jamuMenuCanvasGroup;

    private void Awake()
    {
        SetupCanvasGroups();
    }

    private void Start()
    {
        SetInitialState();
    }

    private void SetupCanvasGroups()
    {
        ingredientsCanvasGroup = GetOrAddCanvasGroup(ingredientsMenu);
        makeCanvasGroup = GetOrAddCanvasGroup(makeMenu);
        cutsceneCanvasGroup = GetOrAddCanvasGroup(cutsceneMenu);
        serveCanvasGroup = GetOrAddCanvasGroup(serveMenu);
        jamuMenuCanvasGroup = GetOrAddCanvasGroup(JamuMenu);
    }

    private CanvasGroup GetOrAddCanvasGroup(RectTransform menu)
    {
        var canvasGroup = menu.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = menu.gameObject.AddComponent<CanvasGroup>();
        }
        return canvasGroup;
    }

    private void SetInitialState()
    {
        JamuMenu.anchoredPosition = JamuMenuOffScreen;
        makeMenu.anchoredPosition = makeMenuOffScreen;
        cutsceneMenu.anchoredPosition = cutsceneOffScreen;
        serveMenu.anchoredPosition = serveOffScreen;
        ingredientsMenu.anchoredPosition = ingredientsOnScreen;

        makeCanvasGroup.alpha = 1;
        cutsceneCanvasGroup.alpha = 0;
        serveCanvasGroup.alpha = 0;
        ingredientsCanvasGroup.alpha = 1;


        ingredientsMenu.gameObject.SetActive(true);
        makeMenu.gameObject.SetActive(true);
    }

    public void ShowJamuMenu()
    {
        Debug.Log("Memulai ShowJamuMenu");
        try
        {
            JamuMenu.gameObject.SetActive(true);
            JamuMenu.DOAnchorPos(JamuMenuOnScreen, slideDuration).SetEase(Ease.OutCubic);
            Debug.Log("ShowJamuMenu berhasil dijalankan");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error di ShowJamuMenu: {e.Message}");
        }
    }
    public void ShowJamuMenuSafely()
    {
        if (isAnimating) return; // Pastikan animasi hanya berjalan sekali
        isAnimating = true;
        ShowJamuMenu(); // Memulai animasi
        isAnimating = false;
    }
    public IEnumerator AnimateBrewingSequence()
    {
        if (isAnimating) yield break;
        isAnimating = true;

        ingredientsCanvasGroup.DOFade(0, fadeDuration);
        ingredientsMenu.gameObject.SetActive(false);

        makeMenu.gameObject.SetActive(true);
        makeCanvasGroup.alpha = 1;
        makeMenu.DOAnchorPos(makeMenuOnScreen, slideDuration).SetEase(Ease.OutCubic);

        foreach (var img in cutsceneImages)
        {
            if (img != null)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0); // Set transparan
            }
        }

        // Tampilkan cutscene panel dan mulai sliding
        cutsceneMenu.gameObject.SetActive(true);
        cutsceneCanvasGroup.alpha = 1;
        cutsceneMenu.DOAnchorPos(cutsceneOnScreen, slideDuration).SetEase(Ease.OutCubic);

        // Tunggu hingga panel cutscene mencapai posisi target
        yield return new WaitForSeconds(slideDuration / 2);

        // Mulai fade-in gambar saat panel sliding
        foreach (var img in cutsceneImages)
        {
            if (img != null)
            {
                Tween fadeInImage = img.DOFade(1, fadeDuration);
                yield return fadeInImage.WaitForCompletion();
            }
        }


        serveMenu.gameObject.SetActive(true);
        serveCanvasGroup.alpha = 1;
        serveMenu.DOAnchorPos(serveOnScreen, slideDuration).SetEase(Ease.OutCubic);

        yield return new WaitForSeconds(slideDuration);

        isAnimating = false;
    }

    public IEnumerator HideMenuJamu()
    {
        yield return JamuMenu.DOAnchorPos(JamuMenuOffScreen, slideDuration)
            .SetEase(Ease.OutExpo)
            .OnComplete(() => { jamuMenuCanvasGroup.alpha = 0; })
            .WaitForCompletion();
        CleanupSequence();
    }

    public IEnumerator CleanupSequence()
    {
        if (isAnimating) yield break;
        isAnimating = true;

        // Slide out serve menu
        Tween slideOutServe = serveMenu.DOAnchorPos(serveOffScreen, slideDuration)
            .SetEase(Ease.InCubic);

        yield return slideOutServe.WaitForCompletion();

        // Reset everything to initial state
        SetInitialState();

        // Slide in ingredients menu
        ingredientsMenu.gameObject.SetActive(true);
        ingredientsCanvasGroup.alpha = 0;

        Sequence resetSequence = DOTween.Sequence();
        resetSequence.Join(ingredientsMenu.DOAnchorPos(ingredientsOnScreen, slideDuration).SetEase(Ease.OutCubic))
                    .Join(ingredientsCanvasGroup.DOFade(1, fadeDuration));

        yield return resetSequence.WaitForCompletion();
        isAnimating = false;
    }

}