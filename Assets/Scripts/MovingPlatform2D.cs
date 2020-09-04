
using UnityEngine;

public class MovingPlatform2D : MonoBehaviour
{
    [SerializeField] MovementType Type = MovementType.MoveTowards;

    [SerializeField] float speed = 1;

    [SerializeField] Transform[] positions = null;

    [Header("On Hit Only")]
    [SerializeField] bool startMoving = false;
    private bool canMove;

    int counter = 0;
    private string direction = "Towards";

    public enum MovementType
    {
        MoveTowards,
        Lerp
    }
    void Start()
    {
        if (positions.Length > 0 && positions != null)
            transform.position = positions[0].position;

        if (startMoving)
            canMove = false;
        else
            canMove = true;
    }
    void Update()
    {
        if (canMove)
        {
            if (positions.Length > 1 && positions != null)
            {
                if ((transform.position - positions[counter].position).magnitude < 0.001)
                {  // we reached the destination!
                   //counter = (counter + 1) % 2; alternate between 0 and 1, this for a 2 positions platform.

                    if (direction.Equals("Towards"))
                        counter++;
                    else if (direction.Equals("Backwards"))
                        counter--;

                    if (counter == 0)
                        direction = "Towards";
                    else if (counter == positions.Length - 1)
                        direction = "Backwards";
                }
                if (Type.Equals(MovementType.MoveTowards))
                    transform.position = Vector3.MoveTowards(transform.position, positions[counter].position, speed * Time.deltaTime);
                else
                    transform.position = Vector3.Lerp(transform.position, positions[counter].position, speed * Time.deltaTime);
            }
        }
    }

    //Path Definition Ahead.
    private void OnDrawGizmos()
    {
        if (positions == null)
            return;

        if (positions.Length > 1)
        {
            for (int i = 0; i < positions.Length - 1; i++)
                Gizmos.DrawLine(positions[i].position, positions[i + 1].position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (startMoving && !canMove)
            canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (startMoving && !canMove)
            canMove = true;
    }
}
