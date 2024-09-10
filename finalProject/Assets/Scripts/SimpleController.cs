using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//simple controller to get you started
public class SimpleController : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Animator animator;

    private Vector3 playerVelocity;
    private bool groundedPlayer = true;
    public static float playerSpeed = 7.0f;
    private float jumpHeight = 1.5f;
    private float gravityValue = -9.81f;
    private bool isJumping = false;


    private void Start()
    {

    }

    void Update()
    {



    //grounded check 
    
    if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            playerVelocity.x = 0;
            playerVelocity.z = 0;
           
        }
        if (!groundedPlayer && !controller.isGrounded)
    {
        animator.SetFloat("Vertical", playerVelocity.y);
        
    }
    else if (!groundedPlayer && controller.isGrounded)
    {
        animator.SetFloat("Vertical", 0);
        groundedPlayer = true;
    }

        if (controller.isGrounded && playerVelocity.y < 0)
        {
            if (isJumping)
            {
                animator.SetBool("IsJumping", false);
                isJumping = false;
            }
        }
        else if (!controller.isGrounded && !isJumping)
        {
            // This will handle the case when Pac-Man is falling without jumping (e.g., walking off a platform)
            animator.SetBool("IsJumping", true);
            isJumping = true;
        }


        //movement
        Vector3 cameraRight = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up).normalized;
    Vector3 cameraForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;
    Vector3 move = Input.GetAxis("Horizontal") * cameraRight + Input.GetAxis("Vertical") * cameraForward;
    move.y = 0;
    controller.Move(move * Time.deltaTime * playerSpeed);
    if (move != Vector3.zero)
    {
        gameObject.transform.forward = move;
        animator.SetFloat("Forward", Mathf.Max(Mathf.Abs(Input.GetAxis("Vertical")), Mathf.Abs(Input.GetAxis("Horizontal"))));
    }

    //jumping
    if (Input.GetButtonDown("Jump") && groundedPlayer)
    {
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        groundedPlayer = false;
        animator.SetFloat("Vertical", playerVelocity.y);
    }
    playerVelocity.y += gravityValue * Time.deltaTime;
    controller.Move(playerVelocity * Time.deltaTime);

    if (Input.GetKey(KeyCode.LeftShift) && groundedPlayer)
    {
        animator.SetBool("Sliding", true);
    }
    else
    {
        animator.SetBool("Sliding", false);
    }
        controller.Move(playerVelocity * Time.deltaTime);

    }
    public void LaunchPlayer(float launchForce)
    {
        // Apply an immediate upward force to the player
        playerVelocity.y = launchForce;
        groundedPlayer = false; // The player is no longer grounded once launched


        // You may want to handle animations or other effects here
        animator.SetFloat("Vertical", playerVelocity.y);
        animator.SetBool("IsJumping", true);
        isJumping = true;


    }

    public void PropelPlayer(float launchAngleDegrees , float speedMultiplier)
    {
        Vector3 launchDirection = Quaternion.AngleAxis(-launchAngleDegrees, transform.right) * transform.forward;
        float launchSpeed = playerSpeed * speedMultiplier; // Assuming playerSpeed represents the current speed

        // Apply calculated launch velocity
        playerVelocity = launchDirection.normalized * launchSpeed;
        playerVelocity.y = Mathf.Sin(launchAngleDegrees * Mathf.Deg2Rad) * launchSpeed; // Adjust for vertical component

        groundedPlayer = false;
        animator.SetFloat("Vertical", playerVelocity.y);
        animator.SetBool("IsJumping", true);
        isJumping = true;

    }
    public void HandleLanding()
    {
        if (controller.isGrounded)
        {
            animator.SetBool("IsJumping", false);
            isJumping = false;
            // Reset the Vertical animator parameter if needed
            animator.SetFloat("Vertical", 0);
        }

       
    }
}

