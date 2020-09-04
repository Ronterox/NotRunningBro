
using UnityEngine;

public class CameraModifierOnHit : MonoBehaviour
{
    [SerializeField] CameraFollow2D cameraFollower = null;

    [SerializeField] CameraFollow2D.FollowType newFollowType = CameraFollow2D.FollowType.Smooth;

    [SerializeField] bool keepSameFollowType = false;

    [SerializeField] CameraFollow2D.OffSetType newOffSetType = CameraFollow2D.OffSetType.Automatic;

    [SerializeField] bool keepSameOffSetType = false;

    [SerializeField] Vector2 newOffSet = Vector2.zero;

    [SerializeField] bool keepSameOffSet = false;

    [Space]
    [Header("On Exit")]
    [SerializeField] bool RestoreDefault = false;

    private CameraFollow2D.FollowType oldFollowType;

    private CameraFollow2D.OffSetType oldOffSetType;

    private Vector2 oldOffSet;

    private void Start()
    {
        if (RestoreDefault)
        {
            oldFollowType = cameraFollower.followType;
            oldOffSetType = cameraFollower.offSetType;
            oldOffSet = cameraFollower.cameraOffSet;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && cameraFollower != null)
        {
                if (cameraFollower.cameraOffSet != newOffSet && !keepSameOffSet)
                    cameraFollower.cameraOffSet = newOffSet;

                if (cameraFollower.followType != newFollowType && !keepSameFollowType)
                    cameraFollower.followType = newFollowType;

                if (cameraFollower.offSetType != newOffSetType && !keepSameOffSetType)
                    cameraFollower.offSetType = newOffSetType;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
                if (cameraFollower.cameraOffSet != newOffSet && !keepSameOffSet)
                    cameraFollower.cameraOffSet = newOffSet;

                if (cameraFollower.followType != newFollowType && !keepSameFollowType)
                    cameraFollower.followType = newFollowType;

                if (cameraFollower.offSetType != newOffSetType && !keepSameOffSetType)
                    cameraFollower.offSetType = newOffSetType;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (RestoreDefault)
            {
                cameraFollower.cameraOffSet = oldOffSet;
                cameraFollower.followType = oldFollowType;
                cameraFollower.offSetType = oldOffSetType;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (RestoreDefault)
            {
                cameraFollower.cameraOffSet = oldOffSet;
                cameraFollower.followType = oldFollowType;
                cameraFollower.offSetType = oldOffSetType;
            }
        }
    }
}
