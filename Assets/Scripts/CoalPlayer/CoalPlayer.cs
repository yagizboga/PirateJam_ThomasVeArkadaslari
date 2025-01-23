using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoalPlayer : MonoBehaviour
{
    private bool isAttacking = false;
    private bool isDigging = false;
    private Animator animator;
    private bool isHoldingShovel = true;
    private bool canDig = true;
    private bool isReadyToCarry = false;

    public GameObject attackShovel;
    public GameObject diggingShovel;
    public GameObject coal;

    private bool isInOven = false;
    private bool isInCoalBox = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        attackShovel.SetActive(true);
        diggingShovel.SetActive(false);
        coal.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!isAttacking && isHoldingShovel && !isDigging)
            {
                isAttacking = true;
                animator.SetBool("isShovelAttacking", true);
                if ((attackShovel.activeSelf == false) || diggingShovel.activeSelf == true)
                {
                    if (!isDigging)
                    {
                        attackShovel.SetActive(true);
                        diggingShovel.SetActive(false);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!isAttacking && isHoldingShovel && !isDigging && canDig)
            {
                canDig = false;
                isDigging = true;
                animator.SetBool("isDigging", true);
                if ((attackShovel.activeSelf == true) || diggingShovel.activeSelf == false)
                {
                    if (!isAttacking)
                    {
                        attackShovel.SetActive(false);
                        diggingShovel.SetActive(true);
                    }
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (isDigging)
            {
                isDigging = false;
                animator.SetBool("isDigging", false);
            }
            if(!isReadyToCarry && coal.activeSelf == true)
            {
                coal.SetActive(false);
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
        animator.SetBool("isShovelAttacking", false);
    }

    public void SetIsDiggingFalse()
    {
        isDigging = false;
        animator.SetBool("isDigging", false);
    }

    public void SetAttackShovel()
    {
        attackShovel.SetActive(true);
        diggingShovel.SetActive(false);
    }

    public void SetDigShovel()
    {
        attackShovel.SetActive(false);
        diggingShovel.SetActive(true);
    }

    public void SetCanDigTrue()
    {
        canDig = true;
        StartCoroutine(ResetCanDig());
    }

    private IEnumerator ResetCanDig()
    {
        yield return new WaitForSeconds(0.15f);
        canDig = true;
    }

    public void SetIsReadyToCarryTrue()
    {
        isReadyToCarry = true;
        animator.SetBool("isReadyToCarry", true);
        
    }

    public void SetCoalActive() 
    {
        if (coal.activeSelf == false && isInCoalBox)
        {
            coal.SetActive(true);
        }
    }

    public void SetCoalDeactive()
    {
        if (coal.activeSelf == true)
        {
            coal.SetActive(false);
            if (isInOven)
            {
                //
                // train speed & fuel amount ++
                //
            }
        }
    }

    public void SetIsReadyToCarryFalse()
    {
        isReadyToCarry = false;
        animator.SetBool("isReadyToCarry", false);
    }

    public void SetInOven(bool inOven)
    {
        isInOven = inOven;
    }

    public void SetInCoalBox(bool inCoalBox)
    {
        isInCoalBox = inCoalBox;
    }
}   
