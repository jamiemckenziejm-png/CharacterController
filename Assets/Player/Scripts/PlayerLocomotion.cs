using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerLocomotion : MonoBehaviour
{
    CharacterController characterController;
    Transform playerContainer, cameraContainer;

    public float speed = 6.0f;
    public float jumpSpeed = 10f;
    public float mouseSensitivity = 0.5f;
    public float gravity = 20.0f;
    public float lookUpClamp = -30f;
    public float lookDownClamp = 60f;

    private Vector3 moveDirection = Vector3.zero;
    float rotateX, rotateY;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction crouchAction;
    InputAction lookAction;

    void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();

        var map = playerInput.currentActionMap;

        moveAction = map.FindAction("Move", true);
        jumpAction = map.FindAction("Jump", true);
        crouchAction = map.FindAction("Crouch", true);
        lookAction = map.FindAction("Look", true);
    }

    void Start()
    {
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
        //SetCurrentCamera();
    }

    void Update()
    {
        Locomotion();

        RotateAndLook();
        //PerspectiveCheck();
    }

    void Locomotion()
    {
        if (characterController.isGrounded) // When grounded, set y-axis to zero (to ignore it)
        {
            Vector2 move = moveAction.ReadValue<Vector2>();
            moveDirection = new Vector3(move.x, 0f, move.y);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (jumpAction.triggered)
            {
                moveDirection.y = jumpSpeed;
            }
            if (crouchAction.IsPressed())
            {
                characterController.height = 0.65f;
                characterController.center = new Vector3(0f, 0.5f, 0f);
            }
            else //if crouch unpressed
            {
                characterController.height = 2f;
                characterController.center = new Vector3(0f, 1f, 0f);
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void RotateAndLook()
    {
        Vector2 look = lookAction.ReadValue<Vector2>();

        rotateX = look.x * mouseSensitivity;
        rotateY -= look.y * mouseSensitivity;

        rotateY = Mathf.Clamp(rotateY, lookUpClamp, lookDownClamp);

        transform.Rotate(0f, rotateX, 0f);

        cameraContainer.transform.localRotation = Quaternion.Euler(rotateY, 0f, 0f);
    }
}