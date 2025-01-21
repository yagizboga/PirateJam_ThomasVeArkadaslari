using UnityEngine;
using UnityEngine.InputSystem;

public class CoalPlayer : MonoBehaviour
{
    private bool isAttacking = false;
    private Animator animator;
    private bool isHoldingShovel = true;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!isAttacking && isHoldingShovel)
            {
                isAttacking = true;
                animator.SetBool("isShovelAttacking", isAttacking);

            }
            
        }
    }

    public void SetIsHoldingShovel(bool hold)
    {
        isHoldingShovel = hold;
    }

    public void SetIsAttackingFalse() 
    { 
        isAttacking = false;
        animator.SetBool("isShovelAttacking", isAttacking);
    }
}
