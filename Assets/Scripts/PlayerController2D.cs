
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float moveSpeed = 5f;

    public float jumpForce = 5f;

    public float speedBoostOnSlider = 3f;

    [SerializeField] float maxVelocity = 25f;

    private bool canJump, canMove;

    private Rigidbody2D playerRigidbody2D;

    private Animator animator;

    private void Start()
    {
        playerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove)
        {
            Jump();

            float moveInput = Input.GetAxisRaw("Horizontal");

            if (moveInput == 0)
                animator.SetBool("isRunning", false);
            else
                animator.SetBool("isRunning", true);

            if (moveInput > 0)
                transform.eulerAngles = new Vector3(0, 0, 0);
            else if (moveInput < 0)
                transform.eulerAngles = new Vector3(0, 180, 0);

            Vector3 movement = new Vector3(moveSpeed * moveInput * Time.deltaTime, 0f, 0f);

            transform.position += movement;
        }
        else if (!canMove)
            animator.SetTrigger("getHit");
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && canJump)
        {
            animator.SetTrigger("takeOff");
            playerRigidbody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            canJump = false;
        }

        if (canJump == false && Input.GetAxisRaw("Vertical") < 0)
            playerRigidbody2D.AddForce(new Vector2(0f, -jumpForce), ForceMode2D.Force);

        if (playerRigidbody2D.velocity.y != 0)
            animator.SetBool("isJumping", true);
        else
            animator.SetBool("isJumping", false);

        if (playerRigidbody2D.velocity.y > maxVelocity)
            playerRigidbody2D.velocity /= 2;

    }
    void JumpBoosted()
    {
        if (Input.GetButtonDown("Jump") && canJump)
        {
            animator.SetTrigger("takeOff");
            playerRigidbody2D.AddForce(new Vector2(0f, jumpForce * 2), ForceMode2D.Impulse);
            canJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        switch (collision.gameObject.tag)
        {
            case "Ground":
                playerRigidbody2D.velocity = Vector2.zero;

                if (!canJump)
                    canJump = true;

                if (!canMove)
                    canMove = true;

                break;

            case "Platform":
                playerRigidbody2D.velocity = Vector2.zero;

                if (!canJump)
                    canJump = true;

                if (!canMove)
                    canMove = true;

                gameObject.transform.parent = collision.transform;

                break;

            case "Jumpad":
                if (!canMove)
                    canMove = true;

                if (canJump)
                    canJump = false;

                animator.SetTrigger("takeOff");

                break;

            case "Trap":

                if (canMove)
                    canMove = false;

                break;

            case "Slider":
                if (!canMove)
                    canMove = true;

                if (!canJump)
                    canJump = true;

                moveSpeed += speedBoostOnSlider;
                jumpForce += speedBoostOnSlider / 2;

                break;

            case "Wall":
                if (!canMove)
                    canMove = true;

                if (!canJump)
                    canJump = true;

                if (!animator.GetBool("isClimbing"))
                    animator.SetBool("isClimbing", true);

                JumpBoosted();

                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
            gameObject.transform.parent = null;
        else if (collision.gameObject.CompareTag("Slider"))
        {
            moveSpeed -= speedBoostOnSlider;
            jumpForce -= speedBoostOnSlider / 2;
        }
        else if (collision.gameObject.CompareTag("Wall"))
            animator.SetBool("isClimbing", false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":

                if (canJump)
                    canJump = false;

                if (!canMove)
                    canMove = true;

                break;

            case "Platform":
                playerRigidbody2D.velocity = Vector2.zero;

                if (!canJump)
                    canJump = true;

                if (!canMove)
                    canMove = true;

                gameObject.transform.parent = collision.transform;

                break;

            case "Jumpad":
                if (!canMove)
                    canMove = true;

                if (canJump)
                    canJump = false;

                animator.SetTrigger("takeOff");

                break;

            case "Trap":
                if (canMove)
                    canMove = false;

                break;

            case "Slider":
                if (!canMove)
                    canMove = true;

                if (!canJump)
                    canJump = true;

                moveSpeed += speedBoostOnSlider;
                jumpForce += speedBoostOnSlider / 2;

                break;

            case "Wall":
                if (!canMove)
                    canMove = true;

                if (!canJump)
                    canJump = true;

                if (!animator.GetBool("isClimbing"))
                    animator.SetBool("isClimbing", true);

                break;
        }
    }
}
