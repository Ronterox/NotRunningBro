

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public HealthBar2D healthBarBoss = null;

    public int maxHealth = 100;

    public int currentHealth;

    public static GameManager instance;

    public GameOver gameOver = null;

    private static int lastScene;

    private static int[] stars;

    [Range(2, 5)] [SerializeField] int maxRangeLoadScene = 5;

    [SerializeField] GameObject pausedMenu = null;

    [SerializeField] GameObject megumin = null;

    float timer = 0;

    bool isPaused = false;

    private void Awake()
    {
        MakeSingleton();
    }

    private void Update()
    {
        int thisScene = SceneManager.GetActiveScene().buildIndex;

        if (thisScene == 0)
        {
            Destroy(healthBarBoss.gameObject);
            Destroy(gameObject);
        }

        GameOverManager();

        CompletedGame();

        PauseGame();
    }

    private void CompletedGame()
    {
        if (currentHealth <= 0)
        {
            AudioManager.instance.Stop("theme");
            AudioManager.instance.Stop("MonsterScream");

            GameObject bunny = GameObject.Find("bunny");
            bunny.GetComponent<Animator>().SetTrigger("Death");
            bunny.GetComponent<AudioSource>().pitch = 3;
            bunny.GetComponent<EnemyScript2D>().enabled = false;
            bunny.GetComponent<Collider2D>().enabled = false;

            Instantiate(megumin, bunny.transform.position, Quaternion.identity);

            timer += Time.deltaTime;
            if (timer >= 4)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void Start()
    {
        if (healthBarBoss != null)
            healthBarBoss.SetMaxHealth(maxHealth);

        currentHealth = maxHealth;

        stars = new int[maxRangeLoadScene];
    }

    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static void LoadScene()
    {
        int scene = UnityEngine.Random.Range(2, instance.maxRangeLoadScene + 1);

        if (stars != null)
        {
            if (StarsLeft() <= 2)
                lastScene = SceneManager.GetActiveScene().buildIndex;

            if (scene != SceneManager.GetActiveScene().buildIndex && scene != lastScene && CheckStars(scene))
            {
                lastScene = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(scene);
            }
            else if (StarsLeft() == 1 && CheckStars(lastScene))
            {
                SceneManager.LoadScene(lastScene);
            }
        }
    }

    public static void TakeDamage(int damage)
    {
        instance.currentHealth -= damage;

        if (instance.healthBarBoss != null)
            instance.healthBarBoss.SetHealth(instance.currentHealth);

        Debug.Log("Character took " + damage + " damage.\nCurrent HP: " + instance.currentHealth);
    }

    public static void PickStar()
    {
        //save  level   in  which   star    was picked.
        for (int i = 0; i < stars.Length; i++)
        {
            if (stars[i] == 0)
            {
                stars[i] = SceneManager.GetActiveScene().buildIndex;
                return;
            }
        }
    }

    private static bool CheckStars(int scene)
    {
        //checks   if   different   from    stars   array   picked.
        for (int i = 0; i < stars.Length; i++)
        {
            if (scene == stars[i])
                return false;
        }
        return true;
    }

    private void GameOverManager()
    {
        if (GameOver.isDead)
        {
            gameOver.gameObject.SetActive(true);
        }
    }

    private static int StarsLeft()
    {
        int counter = 0;

        for (int i = 0; i < stars.Length; i++)
        {
            if (stars[i] != 0)
                counter++;
        }

        return stars.Length - counter;
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameOver.isDead)
        {
            if (isPaused)
            {
                Time.timeScale = 1f;
                isPaused = false;
                pausedMenu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                isPaused = true;
                pausedMenu.SetActive(true);
            }
        }
    }
}
