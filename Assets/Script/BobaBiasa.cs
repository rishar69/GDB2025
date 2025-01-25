using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StirScript : MonoBehaviour
{
    public Image belumMateng; // Image for "not stirred" state
    public Image lagiDiAduk;  // Image for "stirring" state
    public Image udahJadi;    // Image for "finished" state
    public Slider progressBar;   // UI Slider for progress
    public GameObject panelBerhasil;  // Panel for success message
    public float kecepatanIsi = 0.01f; // Speed of progress bar filling
    public float toleransiReset = 0.2f; // Distance tolerance to detect circular movement
    public float kecepatanRotasi = 100f; // Rotation speed for "stirring" image
    public float kecepatanGerakPanel = 2f; // Speed of panel movement
    public int indeksSceneBerikutnya; // Index of the next scene to load

    private Vector3 posisiMouseSebelumnya; // To track mouse movement
    private float progress = 0f; // Progress value
    private bool selesai = false;

    void Start()
    {
        // Initialize UI
        AturKeadaanGambar(belumMateng, true);
        AturKeadaanGambar(lagiDiAduk, false);
        AturKeadaanGambar(udahJadi, false);
        progressBar.value = 0f;

        // Set the initial state of the success panel
        if (panelBerhasil != null)
        {
            RectTransform panelRect = panelBerhasil.GetComponent<RectTransform>();
            if (panelRect != null)
            {
                panelRect.anchoredPosition = new Vector2(1305.016f, panelRect.anchoredPosition.y);
            }
            panelBerhasil.SetActive(false);
        }
    }

    void Update()
    {
        if (selesai) return;

        // Detect circular mouse movement
        Vector3 posisiMouseSaatIni = Input.mousePosition;

        if (Input.GetMouseButton(0)) // Holding the left mouse button
        {
            if (posisiMouseSebelumnya != Vector3.zero)
            {
                // Calculate mouse movement
                Vector3 delta = posisiMouseSaatIni - posisiMouseSebelumnya;

                // If movement is significant
                if (delta.magnitude > toleransiReset)
                {
                    // Increment progress
                    progress += kecepatanIsi * Time.deltaTime;
                    progress = Mathf.Clamp01(progress);

                    // Update the progress bar
                    progressBar.value = progress;

                    // Manage image states
                    if (progress > 0f && progress < 1f)
                    {
                        AturKeadaanGambar(belumMateng, false);
                        AturKeadaanGambar(lagiDiAduk, true);
                        AturKeadaanGambar(udahJadi, false);

                        // Rotate the "stirring" image
                        RotateImage(lagiDiAduk);
                    }

                    // When progress is full
                    if (progress >= 1f)
                    {
                        SelesaiMengaduk();
                    }
                }
            }

            posisiMouseSebelumnya = posisiMouseSaatIni; // Update previous mouse position
        }
        else
        {
            posisiMouseSebelumnya = Vector3.zero; // Reset if the mouse button is not held
        }
    }

    void SelesaiMengaduk()
    {
        selesai = true;

        // Show "finished" image
        AturKeadaanGambar(lagiDiAduk, false);
        AturKeadaanGambar(udahJadi, true);

        // Move success panel
        if (panelBerhasil != null)
        {
            panelBerhasil.SetActive(true);
            StartCoroutine(MovePanelToTarget());
        }

        // Change to the next scene after a short delay
        Invoke("GantiScene", 2f); // 2-second delay before switching scenes
    }

    private IEnumerator MovePanelToTarget()
    {
        RectTransform panelRect = panelBerhasil.GetComponent<RectTransform>();
        Vector2 targetPosition = new Vector2(0f, panelRect.anchoredPosition.y);

        while (Vector2.Distance(panelRect.anchoredPosition, targetPosition) > 0.01f)
        {
            panelRect.anchoredPosition = Vector2.Lerp(panelRect.anchoredPosition, targetPosition, kecepatanGerakPanel * Time.deltaTime);
            yield return null;
        }

        panelRect.anchoredPosition = targetPosition;
    }

    void GantiScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(indeksSceneBerikutnya);
    }

    // Helper method to toggle images
    private void AturKeadaanGambar(Image gambar, bool keadaan)
    {
        if (gambar != null)
        {
            gambar.gameObject.SetActive(keadaan);
        }
    }

    // Method to rotate an image around Z-axis
    private void RotateImage(Image image)
    {
        if (image != null)
        {
            image.transform.Rotate(0f, 0f, kecepatanRotasi * Time.deltaTime);
        }
    }
}