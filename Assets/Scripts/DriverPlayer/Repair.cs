using UnityEngine;

public class Repair : MonoBehaviour
{
    private bool canRepair = false;
    private bool isRepairing = false;
    private Animator animator;
    public GameObject hammer;
    private PlayerMovement playerMovement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        hammer.SetActive(false);
    }

    private void Update()
    {
        if (playerMovement.isActivePlayer)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isRepairing = true;
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                isRepairing = false;
            }

            if (isRepairing && canRepair)
            {
                animator.SetBool("isRepairing", true);
                if (hammer.activeSelf == false)
                {
                    hammer.SetActive(true);
                }
            }
            else
            {
                animator.SetBool("isRepairing", false);
                if (hammer.activeSelf == true)
                {
                    hammer.SetActive(false);
                }
            }
        }
    }

    public void SetCanRepair(bool cann)
    {
        canRepair = cann;
    }
}
