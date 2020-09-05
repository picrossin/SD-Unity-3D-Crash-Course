using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Fields
    [SerializeField] [Range(0.0f, 5.0f)] private float mouseSensitivity = 1.0f;

    // Game object dependencies
    [SerializeField]
    private GameObject playerGameObject = null;

    // Input
    private PlayerInputActions inputActions;
    private Vector2 lookInput;

    private float verticalRotation = 0.0f;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    private void Update()
    {
        lookInput = inputActions.Player.Look.ReadValue<Vector2>() * mouseSensitivity;

        verticalRotation -= lookInput.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        playerGameObject.transform.Rotate(Vector3.up * lookInput.x);
    }
}
