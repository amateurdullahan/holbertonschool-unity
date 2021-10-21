using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // movement
    public CharacterController controller;
    public float speed = 15f;
    // facing
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    // gravity
    public float gravity = -18f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight = 5f;
    bool doubleJump = true;
    public bool lockmovement = false;
    // respawn
    public Transform spawnPoint;
    public GameObject thePlayer;
    // pause menu
    public PauseMenu pm;
    public GameObject winCanvas;
    public Animator animate;
    public AudioSource footsteps;

    void Start()
    {
        foreach (Animator ani in GetComponentsInChildren<Animator>())
        {
            animate = ani;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && winCanvas.activeSelf == false)
        {
            if (pm.gameIsPaused)
            {
                pm.Resume();
            }
            else
            {
                pm.Pause();
            }
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        animate.SetBool("isGrounded", isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animate.SetBool("isLanding", true);
            lockmovement = false;
        }
        else
        {
            animate.SetBool("isLanding", false);
        }

        float hor = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(hor, 0f, vert).normalized;
        if (direction.magnitude >= 0.1f && lockmovement == false)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            animate.SetBool("isMoving", true);

            if (isGrounded && !footsteps.isPlaying)
            {
                footsteps.Play();
            }
        }
        else
        {
            animate.SetBool("isMoving", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animate.SetTrigger("isJumping");
        }
        if (Input.GetButtonDown("Jump") && isGrounded == false && doubleJump == true)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animate.SetTrigger("isJumping");
            doubleJump = false;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (groundCheck.position.y < -20)
        {
            animate.SetTrigger("isFalling");
            thePlayer.transform.position = spawnPoint.transform.position;
            footsteps.Play();
            lockmovement = true;
        }

        doubleJump = true;
    }
}
