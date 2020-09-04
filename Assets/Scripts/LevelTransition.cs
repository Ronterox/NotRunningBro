
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] float delay = 0f;

    private float timer;

    bool startTimer;

    private void Update()
    {
        if (startTimer) {
            timer += Time.deltaTime;

            if (timer >= delay)
                GameManager.LoadScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        startTimer = true;
    }
}
