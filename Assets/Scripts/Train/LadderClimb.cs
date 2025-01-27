using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 3f;
    private Rigidbody rb; 
    private PlayerMovement playerMovement;
    private bool onLadder = false;
    private float verticalInput;
    private float horizontalInput;
    private bool inTrigger = false;


    void Update()
    {
        Debug.Log(verticalInput);
        if (onLadder)
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");    

            if (verticalInput != 0 || horizontalInput != 0)
            {
                rb.linearVelocity = new Vector3(0, verticalInput * climbSpeed, 0 - (horizontalInput * horizontalSpeed));
            }
            else
            {
                rb.linearVelocity = new Vector3(0, 0, 0);
            }
        }
        if(playerMovement != null && playerMovement.GetIsGrounded() == true && (verticalInput < 0))
        {
            onLadder = false;
            if (playerMovement != null)
            {
                playerMovement.SetOnLadder(false);
            }
            rb.useGravity = true;
        }
        if(inTrigger && verticalInput > 0 && onLadder == false)
        {
            onLadder = true;
            if (playerMovement != null)
            {
                playerMovement.SetOnLadder(true);
            }
            rb.linearVelocity = Vector3.zero;
            rb.useGravity = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("shooter")) 
        {
            rb = other.GetComponent<Rigidbody>();
            playerMovement = other.GetComponent<PlayerMovement>();
            onLadder = true;
            if (playerMovement != null)
            {
                playerMovement.SetOnLadder(true);
            }
            rb.useGravity = false; 
            rb.linearVelocity = Vector3.zero;
            inTrigger = true;
        }
        if (other.CompareTag("DriverPlayer"))
        {
            rb = other.GetComponent<Rigidbody>();
            playerMovement = other.GetComponent<PlayerMovement>();
            onLadder = true;
            if (playerMovement != null)
            {
                playerMovement.SetOnLadder(true);
            }
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            inTrigger = true;
        }
        if (other.CompareTag("CoalPlayer"))
        {
            rb = other.GetComponent<Rigidbody>();
            playerMovement = other.GetComponent<PlayerMovement>();
            onLadder = true;
            if (playerMovement != null)
            {
                playerMovement.SetOnLadder(true);
            }
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            inTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("shooter") || other.CompareTag("DriverPlayer") || other.CompareTag("CoalPlayer"))
        {
            if(onLadder == false)
            {
                verticalInput = Input.GetAxis("Vertical");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("shooter") || other.CompareTag("DriverPlayer") || other.CompareTag("CoalPlayer"))
        {
            onLadder = false;
            if(playerMovement != null)
            {
                playerMovement.SetOnLadder(false);
            }
            rb.useGravity = true;
            inTrigger = false;
        }
    }
}
