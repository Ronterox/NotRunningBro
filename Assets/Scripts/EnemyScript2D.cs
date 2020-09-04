
using UnityEngine;

public class EnemyScript2D : MonoBehaviour
{
    [SerializeField] Transform playerPosition = null;

    [SerializeField] float aggroRange = 0f;

    [SerializeField] float moveSpeed = 5f;

    [SerializeField] MovesTo movesTo = MovesTo.Everywhere;

    public enum MovesTo
    {
        Everywhere,
        JustInFront
    }

    private void Update()
    {
        float distanceBtwPlayer = Vector3.Distance(transform.position, playerPosition.position);

        if (distanceBtwPlayer <= aggroRange)
            chasePlayer();
    }

    private void chasePlayer()
    {
        if (movesTo.Equals(MovesTo.JustInFront))
        {
            if (transform.position.x < playerPosition.position.x)
                transform.position += new Vector3(1f, 0f, transform.position.z) * Time.deltaTime * moveSpeed;
            else
                transform.position += new Vector3(-1f, 0f, transform.position.z) * Time.deltaTime * moveSpeed;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (playerPosition == null)
            return;

        float distanceBtwPlayer = Vector3.Distance(transform.position, playerPosition.position);

        if (distanceBtwPlayer <= aggroRange)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameOver.isDead = true;
        collision.gameObject.SetActive(false);
        AudioManager.instance.Play("death");
        AudioManager.instance.Stop("theme");
    }
}
