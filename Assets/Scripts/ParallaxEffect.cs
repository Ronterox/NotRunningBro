
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] Transform cam = null;

    [SerializeField][Range(0.1f,0.9f)] float parallaxEffect = 0.5f;

    private Vector3 lastCameraPosition;

    void Start()
    {
        if (cam == null)
            Debug.Log("The camera for the parallax hasn't been set yet.");

        lastCameraPosition = cam.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cam.position - lastCameraPosition;

        transform.position += deltaMovement * parallaxEffect;

        lastCameraPosition = cam.position;
    }
}
