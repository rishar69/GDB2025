using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {

        AudioManager.Instance.PlayBgm("BGM1");
    }
    public void StartButton(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
