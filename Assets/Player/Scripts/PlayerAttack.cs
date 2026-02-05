using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    Transform cameraTransform; // Reference to the camera's transform component
    float range = 100f; // The maximum distance the player's attack can reach

    [SerializeField]
    float rawDamage = 10f; // The raw damage value that will be sent to the enemy when hit


    PlayerInput playerInput; // Reference to the PlayerInput component for handling player input
    InputAction attackAction; // Reference to the specific input action for attacking, which will be assigned in the OnEnable method

    void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>(); // Get the PlayerInput component attached to the same game object

        var map = playerInput.currentActionMap; // Get the current action map from the PlayerInput component, which contains all the input actions defined for the player

        attackAction = map.FindAction("Attack", true); // Find the specific input action named "Attack" within the current action map and assign it to the attackAction variable. The second parameter 'true' indicates that an exception will be thrown if the action is not found.
    }

    void Update() // This method is called once per frame and is responsible for checking if the attack action has been triggered and calling the FireWeapon method if it has
    {
        FireWeapon();
    }

    void FireWeapon() // This method is responsible for handling the logic of firing the player's weapon when the attack action is triggered
    {
        if (attackAction.triggered)
        {
            cameraTransform = Camera.main.transform; // Get the transform component of the main camera, which will be used to determine the direction of the attack
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit, range)) // Perform a raycast from the camera's position in the forward direction, and if it hits an object within the specified range, store the hit information in the raycastHit variable
            {
                if (raycastHit.transform != null)
                {
                    raycastHit.collider.SendMessageUpwards("Hit", rawDamage, SendMessageOptions.DontRequireReceiver); // If the raycast hits an object, send a message upwards through the hit object's hierarchy to call the "Hit" method on any component that has it, passing the rawDamage value as a parameter. The SendMessageOptions.DontRequireReceiver option means that if no component in the hierarchy has a "Hit" method, no error will be thrown.

                }
            }
            else
            {
                Debug.Log("NO RAYCAST");
            }
        }
    }
}