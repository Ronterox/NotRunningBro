
using UnityEngine;

public class KnockbackOnTouch2D : MonoBehaviour
{
    [SerializeField] float knockbackForce = 5f;

    [SerializeField][Tooltip("The gameObject has to have the tag \"Trap\" to be able to do damage")] 
    int damageDealt = 0;

    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("Jumpad"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,
                (collision.gameObject.transform.position.y - gameObject.transform.position.y) * knockbackForce),
                ForceMode2D.Impulse);

            AudioManager.instance.Play("jumpad");
            animator.SetTrigger("jumpad");
        }

        if (gameObject.CompareTag("Trap"))
        {
            //Here we substract the position of the object hit in relationship with our object TIMES the knockbackForce
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                (collision.gameObject.transform.position.x - gameObject.transform.position.x) * knockbackForce,
                (collision.gameObject.transform.position.y - gameObject.transform.position.y) * knockbackForce
                ), ForceMode2D.Impulse);

            collision.gameObject.GetComponent<HealthManager2D>().TakeDamage(damageDealt);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Trap"))
        {
            Rigidbody2D collisionRigid = collision.GetComponent<Rigidbody2D>();
            //Here we substract the position of the object hit in relationship with our object TIMES the knockbackForce

            collisionRigid.velocity = Vector2.zero;

            collisionRigid.AddForce(new Vector2(
                (collision.transform.position.x - gameObject.transform.position.x) * knockbackForce,
                (collision.transform.position.y - gameObject.transform.position.y) * knockbackForce
                ), ForceMode2D.Impulse);

            collision.GetComponent<HealthManager2D>().TakeDamage(damageDealt);
        }
    }
}
