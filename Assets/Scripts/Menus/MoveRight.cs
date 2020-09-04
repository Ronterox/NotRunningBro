
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    [SerializeField] Animator animator = null;
    void Update()
    {
        if (!animator.GetBool("isRunning"))
            animator.SetBool("isRunning", true);
    }
}
