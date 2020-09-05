using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Dependencies
    [SerializeField] private GameObject groundCheck = null;

    // Fields
    [Header("Movement")]
    [SerializeField] private LayerMask ground = 0;
    [SerializeField] [Range(1.0f, 20.0f)] private float movementSpeed = 8.0f;
    [SerializeField] [Range(0.0f, 10.0f)] private float jumpHeight = 3.0f;
    [SerializeField] [Range(-20.0f, 0.0f)] private float gravity = -9.81f;

    // Input
    private PlayerInputActions inputActions;
    private Vector2 movementInput;

    // Components
    private CharacterController controller;

    // Movement
    private bool grounded = false, jump = false;
    private float collisionSphereRadius = 0.5f;
    private Vector3 movement;
    private float yVelocity = 0.0f;

    private void Awake()
    {
        // Input mapping
        inputActions = new PlayerInputActions();
        inputActions.Player.Move.performed += context => movementInput = context.ReadValue<Vector2>();
        inputActions.Player.Jump.performed += context => jump = context.ReadValueAsButton();

        controller = GetComponent<CharacterController>();
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    private void Update()
    {
        grounded = Physics.CheckSphere(groundCheck.transform.position, collisionSphereRadius, ground);

        // Horizontal movement
        movement = transform.right * movementInput.x + transform.forward * movementInput.y;

        controller.Move(movement * movementSpeed * Time.deltaTime);

        // Vertical movement
        if (grounded)
        {
            yVelocity = 0f;
        }

        if (jump && grounded)
        {
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        yVelocity += gravity * Time.deltaTime;
        controller.Move(new Vector3(0, yVelocity, 0) * Time.deltaTime);
        
    }
}
