
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static bool isDead;
    [SerializeField] GameManager gameManager = null;

    private void Update()
    {
        RestartGame();
    }

    private void RestartGame()
    {
        if (Input.GetKeyDown("r") && isDead)
        {
            Destroy(gameManager.gameObject);
            SceneManager.LoadScene(1);
            isDead = false;
            gameObject.SetActive(false);
            AudioManager.instance.Play("theme");
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
        gameObject.SetActive(false);
        isDead = false;
        Debug.Log("LoaadMen");
    }
}
