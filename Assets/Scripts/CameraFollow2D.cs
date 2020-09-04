
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] GameObject player = null;

    public FollowType followType = FollowType.Smooth;

    public float speed = 5f;

    public OffSetType offSetType = OffSetType.Automatic;

    [Tooltip("Usually a positive value means right, and for height is the other way around")]
    public Vector2 cameraOffSet = Vector2.zero;

    private Vector3 playerLastPosition;

    public enum FollowType
    {
        JustFollow,
        MoveTowards,
        Lerp,
        Smooth
    }

    public enum OffSetType
    {
        Fixed,
        Automatic
    }

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("The camera doesn't have an object to follow.");
            return;
        }

        playerLastPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }
    void Update()
    {
        //Created a new vector "playerPosition" instead of using player whole position so the camera will have its normal z in case of Movetowards, or Lerp.
        //I sum Camera off set variable so we can have some off set xD.

        if (offSetType.Equals(OffSetType.Automatic))
        {
            //Left Right
            if (player.transform.position.x > playerLastPosition.x && cameraOffSet.x < 0 || player.transform.position.x < playerLastPosition.x && cameraOffSet.x > 0)
                cameraOffSet.x *= -1;

            //Height
            if (player.transform.position.y > playerLastPosition.y && cameraOffSet.y < 0 || player.transform.position.y < playerLastPosition.y && cameraOffSet.y > 0)
                cameraOffSet.y *= -1;
        }

        Vector3 playerPosition = new Vector3(player.transform.position.x + cameraOffSet.x, player.transform.position.y + cameraOffSet.y, transform.position.z);

        switch (followType)
        {
            case FollowType.JustFollow:
                transform.position = playerPosition;
                break;

            case FollowType.MoveTowards:
                transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
                break;

            case FollowType.Lerp:
                transform.position = Vector3.Lerp(transform.position, playerPosition, speed * Time.deltaTime);
                break;
            case FollowType.Smooth:
                Vector3 velocity = Vector3.zero;
                transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref velocity, speed * Time.deltaTime);
                break;
        }

        //We reset last position.
        playerLastPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }
}
