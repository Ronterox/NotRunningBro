
using UnityEngine;

public class StarScript : MonoBehaviour
{
    [SerializeField] int damageToBoss = 25;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.PickStar();
            AudioManager.instance.Play("MonsterScream");
            Destroy(gameObject);
            GameManager.TakeDamage(damageToBoss);
        }
    }
}
