
using UnityEngine;

public class CameraSizeModifierOnHit : MonoBehaviour
{
    [SerializeField] new GameObject camera = null;

    [SerializeField] float newSize = 5f;

    [Space][Header("On Exit")]
    [SerializeField] bool RestoreDefault = false;

    private Camera cameraComponent;

    private float oldSize;
    private Vector3 oldLocaleScale;

    private void Start()
    {
        cameraComponent = camera.GetComponent<Camera>();
        if (RestoreDefault)
        {
            oldSize = cameraComponent.orthographicSize;
            oldLocaleScale = camera.transform.localScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (cameraComponent.orthographicSize != newSize)
            {
                if (newSize > cameraComponent.orthographicSize)
                {
                    float newXYScale = (newSize - cameraComponent.orthographicSize) * 2 / 10;
                    camera.transform.localScale = new Vector3(
                        camera.transform.localScale.x + newXYScale,
                        camera.transform.localScale.y + newXYScale,
                        camera.transform.localScale.z);
                }
                else
                {
                    float newXYScale = (cameraComponent.orthographicSize - newSize) * 2 / 10;
                    camera.transform.localScale = new Vector3(
                        camera.transform.localScale.x - newXYScale,
                        camera.transform.localScale.y - newXYScale,
                        camera.transform.localScale.z);
                }
                cameraComponent.orthographicSize = newSize;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (cameraComponent.orthographicSize != newSize)
            {
                if (newSize > cameraComponent.orthographicSize)
                {
                    float newXYScale = (newSize - cameraComponent.orthographicSize) * 2 / 10;
                    camera.transform.localScale = new Vector3(
                        camera.transform.localScale.x + newXYScale,
                        camera.transform.localScale.y + newXYScale,
                        camera.transform.localScale.z);
                }
                else
                {
                    float newXYScale = (cameraComponent.orthographicSize - newSize) * 2 / 10;
                    camera.transform.localScale = new Vector3(
                        camera.transform.localScale.x - newXYScale,
                        camera.transform.localScale.y - newXYScale,
                        camera.transform.localScale.z);
                }
                cameraComponent.orthographicSize = newSize;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (RestoreDefault)
            {
                cameraComponent.orthographicSize = oldSize;
                camera.transform.localScale = oldLocaleScale;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (RestoreDefault)
            {
                cameraComponent.orthographicSize = oldSize;
                camera.transform.localScale = oldLocaleScale;
            }
        }
    }
}
