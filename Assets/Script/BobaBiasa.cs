using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StirScript : MonoBehaviour
{
    public Image belumMateng;       // Image for "not stirred" state
    public Image lagiDiAduk;        // Image for "stirring" state
    public Image udahJadi;          // Image for "finished" state
    public Slider progressBar;      // UI Slider for progress
    public TMP_Text pesanBerhasil;  // UI Text for success message
    public TMP_Text pesanAwal;      // UI Text for the initial message
    public float kecepatanIsi = 0.01f; // Speed of progress bar filling
    public float toleransiReset = 0.2f; // Distance tolerance to detect circular movement
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
        pesanBerhasil.gameObject.SetActive(false);

        // Show the initial message for 3 seconds
        if (pesanAwal != null)
        {
            pesanAwal.gameObject.SetActive(true);
            Invoke("SembunyikanPesanAwal", 3f);
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

        // Show success message
        pesanBerhasil.gameObject.SetActive(true);
        Debug.Log("Adukan selesai!");

        // Change to the next scene after a short delay
        Invoke("GantiScene", 2f); // 2-second delay before switching scenes
    }

    void GantiScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(indeksSceneBerikutnya);
    }

    void SembunyikanPesanAwal()
    {
        if (pesanAwal != null)
        {
            pesanAwal.gameObject.SetActive(false);
        }
    }

    // Helper method to toggle images
    private void AturKeadaanGambar(Image gambar, bool keadaan)
    {
        if (gambar != null)
        {
            gambar.gameObject.SetActive(keadaan);
        }
    }
}