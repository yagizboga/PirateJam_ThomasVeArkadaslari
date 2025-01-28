using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isActivePlayer = false;
    public Animator animator;
    public GameObject main_camera;

    private float moveSpeed = 7f;
    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    Vector3 moveDirection;

    private Rigidbody rb;

    public float playerHeight = 2f;
    public LayerMask groundLayer;
    private bool isGrounded = true;
    private float groundDrag = 5f;

    private float jumpForce = 12f;
    private float jumpCoolDown = 0.25f;
    private float airMultiplier = 0.4f;
    private bool readyToJump = true;

    private bool onLadder = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        main_camera.SetActive(false);
    }

    private void Update()
    {
        if (isActivePlayer)
        {
            animator.SetBool("isActivePlayer", true);
            if (main_camera.activeSelf == false)
            {
                main_camera.SetActive(true);
            }
            GroundCheck();
            GetInput();
        }
        else
        {
            if (main_camera.activeSelf == true)
            {
                main_camera.SetActive(false);
            }
            animator.SetBool("isActivePlayer", false);
        }
    }

    

    private void FixedUpdate()
    {
        if (isActivePlayer)
        {
            MovePlayer();
            //RotatePlayer();
        }
    }

    private void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
        if (isGrounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0f;
        }
        //Debug.Log(isGrounded);
        animator.SetBool("inAir", !isGrounded);
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && readyToJump && isGrounded)
        {
            Jump();
            Invoke("ResetJump", jumpCoolDown);
        }
    }

    private void MovePlayer()
    {
        if (!onLadder)  
        {
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
            if (isGrounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

            }
            else if (!isGrounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            }
            SpeedControl();
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
        animator.SetFloat("speed", flatVel.magnitude);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        readyToJump = false;
        animator.SetBool("isJumping", true);
    }

    private void ResetJump()
    {
        //readyToJump = true;
        animator.SetBool("isJumping", false);
        readyToJump = true;
    }

    private void RotatePlayer()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, main_camera.transform.rotation.y, transform.rotation.z);
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public void SetOnLadder(bool ladder)
    {
        onLadder = ladder;
    }

    public void SetIsActivePlayer(bool isActive) 
    {
        isActivePlayer = isActive;
        main_camera.SetActive(isActive);
    }
}
