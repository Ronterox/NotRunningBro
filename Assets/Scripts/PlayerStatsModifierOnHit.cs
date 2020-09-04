
using UnityEngine;

public class PlayerStatsModifierOnHit : MonoBehaviour
{
    [SerializeField] PlayerController2D playerController = null;

    [SerializeField] float newMoveSpeed = 5f;
    [SerializeField] bool keepSameMoveSpeed = false;

    [SerializeField] float newJumpForce = 5f;
    [SerializeField] bool keepSameJumpForce = false;

    [SerializeField] float newSpeedBoostOnSlider = 3f;
    [SerializeField] bool keepSameBoost = false;

    [Header("On Exit")]
    [SerializeField] bool restoreDefault = false;

    private float oldMoveSpeed;

    private float oldJumpForce;

    private float oldSpeedBoostOnSlider;

    void Start()
    {
        if (restoreDefault)
        {
            oldMoveSpeed = playerController.moveSpeed;
            oldJumpForce = playerController.jumpForce;
            oldSpeedBoostOnSlider = playerController.speedBoostOnSlider;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (newMoveSpeed != playerController.moveSpeed && !keepSameMoveSpeed)
            playerController.moveSpeed = newMoveSpeed;

        if (newJumpForce != playerController.jumpForce && !keepSameJumpForce)
            playerController.jumpForce = newJumpForce;

        if (newSpeedBoostOnSlider != playerController.moveSpeed && !keepSameBoost)
            playerController.speedBoostOnSlider = newSpeedBoostOnSlider;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (newMoveSpeed != playerController.moveSpeed && !keepSameMoveSpeed)
            playerController.moveSpeed = newMoveSpeed;

        if (newJumpForce != playerController.jumpForce && !keepSameJumpForce)
            playerController.jumpForce = newJumpForce;

        if (newSpeedBoostOnSlider != playerController.moveSpeed && !keepSameBoost)
            playerController.speedBoostOnSlider = newSpeedBoostOnSlider;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (restoreDefault)
        {
            playerController.moveSpeed = oldMoveSpeed;
            playerController.jumpForce = oldJumpForce;
            playerController.speedBoostOnSlider = oldSpeedBoostOnSlider;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (restoreDefault)
        {
            playerController.moveSpeed = oldMoveSpeed;
            playerController.jumpForce = oldJumpForce;
            playerController.speedBoostOnSlider = oldSpeedBoostOnSlider;
        }
    }
}
