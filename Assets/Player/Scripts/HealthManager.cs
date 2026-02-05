using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    float hitPoints = 100f; // The player's health points

    void Hit(float rawDamage) // This method is called when the player takes damage
    {
        hitPoints -= rawDamage;

        Debug.Log("OUCH: " + hitPoints.ToString()); // Log the current hit points after taking damage

        if (hitPoints <= 0f)
        {
            Debug.Log("TODO: GAME OVER - YOU DIED");
        }
    }
}
