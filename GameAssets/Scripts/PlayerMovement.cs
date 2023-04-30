using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{

    // Variable setups
    private Vector3 playerMovement;
    private CharacterController characterController;
    public float ySpeed;
    public float _gravity = -9.81f;
    public float groundDistance = 0.2f;
    public LayerMask _ground;


    public int maxHealth = 100;
    public int currentHealth;

    public bool damaged;
    private bool canBeDamaged;
    public float damageTimer = 5f;

    public Health_slider healthbar;

    private Animator animator;
    private bool _isGrounded = true;
    private Transform _groundChecker;
    private bool isJumping;

    [SerializeField] private Transform cam;
    [SerializeField] private float rotSpeed = 720f;
    [Space]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight;



    // Variables assigned at game start up
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        _groundChecker = transform.GetChild(0);
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);
    }
    private void Update()
    {

        if (currentHealth > 0) {
            Move();
        } else {
            animator.SetBool("Defeated", true);
            FindObjectOfType<GameManager>().EndGame();
        }

        if (damaged && canBeDamaged)
        {
            TakeDamage(20);
            damaged = false;
            animator.SetBool("isMoving", false);
            animator.SetBool("Damaged", true);
            
        }
        else
        {
            animator.SetBool("Damaged", false);
        }

        if (!canBeDamaged & damageTimer <= 0f)
        {
            DamageReset();
        }

        damageTimer -= Time.deltaTime;

    }

    private void Move()
    {
        // Player ground movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 movementDir = new Vector3(horizontalInput, 0f, verticalInput);
        float magnitude = Mathf.Clamp01(movementDir.magnitude) * moveSpeed;
        movementDir = Quaternion.AngleAxis(cam.rotation.eulerAngles.y, Vector3.up) * movementDir;
        movementDir.Normalize();

        // Checks if the player is on the ground
        _isGrounded = Physics.CheckSphere(_groundChecker.position, groundDistance, _ground, QueryTriggerInteraction.Ignore);

        // Player jumping 
        if (_isGrounded && ySpeed <= 0)
        {
            ySpeed = 0f;
            isJumping = false;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
            animator.SetBool("onGround", true);

            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                animator.SetBool("isJumping", true);
                animator.SetBool("onGround", false);
                Jump();
                isJumping = true;
            }
        }

        if ((isJumping && ySpeed < jumpHeight) || ySpeed < 0)
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("onGround", false);
        }

        ySpeed += _gravity * Time.deltaTime;

        Vector3 velocity = movementDir * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (movementDir != Vector3.zero)
        {
            animator.SetBool("isMoving", true);

            Quaternion rotateTo = Quaternion.LookRotation(movementDir, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTo, rotSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }


    void Jump()
    {
        ySpeed += Mathf.Sqrt(jumpHeight * -2f * _gravity);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.setHealth(currentHealth);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("HitBox"))
        {
            damaged = true;
            canBeDamaged = false;
        }
    }

    private void DamageReset()
    {
        canBeDamaged = true;
        damageTimer = 5f;
    }

}