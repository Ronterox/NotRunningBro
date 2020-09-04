
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.Play("mainMenu");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Closed Game");
        Application.Quit();
    }

    public void PlayTheme()
    {
        AudioManager.instance.Play("theme");
        AudioManager.instance.Stop("mainMenu");
    }
}
