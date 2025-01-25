using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageSequence : MonoBehaviour
{
    public Image[] images; // Array untuk menyimpan semua gambar
    public TMP_Text feedbackText; // Teks untuk memberikan evaluasi
    public int maxTaps = 3; // Jumlah tap maksimum

    private int currentTap = 0; // Jumlah tap yang telah dilakukan
    private int correctTaps = 0; // Jumlah tap yang benar

    void Start()
    {
        // Pastikan semua gambar dimatikan di awal, kecuali yang pertama
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
        }
        if (images.Length > 0)
        {
            images[0].gameObject.SetActive(true); // Nyalakan gambar pertama
        }

        feedbackText.text = ""; // Kosongkan teks feedback di awal
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentTap < maxTaps)
        {
            HandleTap();
        }
    }

    void HandleTap()
    {
        // Matikan gambar sebelumnya
        if (currentTap < images.Length)
        {
            images[currentTap].gameObject.SetActive(false);
        }

        // Tambahkan jumlah tap
        currentTap++;

        // Simulasikan apakah tap benar atau tidak
        // Gantilah logika ini sesuai kondisi sebenarnya
        if (Random.value > 0.5f) // 50% kemungkinan benar
        {
            correctTaps++;
        }

        // Nyalakan gambar berikutnya jika masih dalam batas
        if (currentTap < images.Length)
        {
            images[currentTap].gameObject.SetActive(true);
        }

        // Berikan evaluasi setelah tap terakhir
        if (currentTap >= maxTaps)
        {
            GiveFeedback();
        }
    }

    void GiveFeedback()
    {
        if (correctTaps == 3)
        {
            feedbackText.text = "Sangat Bagus!";
            feedbackText.color = Color.green;
        }
        else if (correctTaps == 2)
        {
            feedbackText.text = "Bagus!";
            feedbackText.color = Color.yellow;
        }
        else if (correctTaps == 1)
        {
            feedbackText.text = "Bagus!";
            feedbackText.color = new Color(1f, 0.5f, 0f); // Warna oranye
        }
        else
        {
            feedbackText.text = "Buruk!";
            feedbackText.color = Color.red;
        }
    }
}