using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField]
    Transform playerTransform; // Reference to the player's transform component, which will be assigned in the Unity editor
    Transform gunTransform; // Reference to the enemy's gun transform component, which will be assigned in the Start method
    float maxDistanceToTarget = 6f; // The maximum distance at which the enemy can attack the player
    float distanceToTarget; // Variable to store the current distance from the enemy to the player

    [SerializeField]
    float rawDamage = 10f; // The raw damage value that will be sent to the player when hit

    [SerializeField]
    float delayTimer = 2f; // The delay time between enemy attacks
    float tick;
    bool attackReady = true; // Flag to indicate whether the enemy is ready to attack

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tick = delayTimer; // Initialize the tick variable with the value of delayTimer
        gunTransform = gameObject.transform.Find("Gun"); // Find the child transform named "Gun" under the enemy game object and assign it to the gunTransform variable
    
    }

    // Update is called once per frame
    void Update()
    {
        Atttack(); // Call the Atttack method to handle the enemy's attack logic
    }

    bool IsreadyToAtack()
    {
        if (tick < delayTimer)
        {
            tick += Time.deltaTime; // Increment the tick variable by the time elapsed since the last frame
            return false; // Return false if the enemy is not ready to attack yet
        }
        return true; // Return true if the enemy is ready to attack
    }

    void LookAtTarget()
    {
        //this.trannsform.LookAt(playerTransform.postition); // Rotate the enemy to face the player's position
        Vector3 lookVector = playerTransform.position - transform.position; // Calculate the vector from the enemy to the player
        lookVector.y = transform.position.y; // Keep the enemy's y-axis rotation unchanged to prevent tilting
        Quaternion rotation = Quaternion.LookRotation(lookVector); // Create a rotation that looks in the direction of the lookVector
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.01f); // Smoothly rotate the enemy towards the player using spherical linear interpolation (Slerp) with a small step value for smoothness
    }

    void Atttack()
    {
        distanceToTarget = Vector3.Distance(transform.position, gunTransform.position); // Calculate the distance from the enemy to the player's position using the gun's position as a reference point
        attackReady = IsreadyToAtack(); // Check if the enemy is ready to attack by calling the IsreadyToAtack method

        if (distanceToTarget <= maxDistanceToTarget)
        {
            LookAtTarget(); // Rotate the enemy to face the player by calling the LookAtTarget method

            if (attackReady)
            {
                tick = 0f; // Reset the tick variable to start the attack cooldown
                Ray ray = new Ray(gunTransform.position, gunTransform.forward); // Create a ray starting from the gun's position and pointing in the forward direction of the gun
                RaycastHit raycastHit;

                if ( Physics.Raycast(ray, out raycastHit, maxDistanceToTarget))
                {
                    Debug.Log("ENEMY SHOOTS");
                    if (raycastHit.transform != null)
                    {
                        raycastHit.collider.SendMessageUpwards("Hit", rawDamage, SendMessageOptions.DontRequireReceiver); // If the raycast hits an object, send a message upwards through the hit object's hierarchy to call the "Hit" method on any component that has it, passing the rawDamage value as a parameter. The SendMessageOptions.DontRequireReceiver option means that if no component in the hierarchy has a "Hit" method, no error will be thrown.
                    }
                }
                else 
                {
                    Debug.Log("ENEMY: FAILED RAYCAST");
                }
            }
        }
    }
}
