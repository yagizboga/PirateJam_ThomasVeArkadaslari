using UnityEngine;

public class Repair : MonoBehaviour
{
    private bool canRepair = false;
    private bool isRepairing = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isRepairing = true;
        }

        if (isRepairing)
        {
            animator.SetBool("isRepairing", true);
        }
        else
        {
            animator.SetBool("isRepairing", false);
        }
    }
}
