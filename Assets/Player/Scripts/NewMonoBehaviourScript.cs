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

    void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();

        var map = playerInput.currentActionMap;

        moveAction = map.FindAction("Move", true);
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

        //RotateAndLook();
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

            //Todo Jumping/Crouching
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}