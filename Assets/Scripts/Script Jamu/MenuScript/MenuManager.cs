using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject settingsPanel; // Panel pengaturan

    public void StartGame()
    {
        SceneManager.LoadScene("SimpleScene"); // Ganti dengan nama scene game Anda
    }

    public void QuitGame()
    {
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true); // Tampilkan panel pengaturan
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false); // Sembunyikan panel pengaturan
    }
}
