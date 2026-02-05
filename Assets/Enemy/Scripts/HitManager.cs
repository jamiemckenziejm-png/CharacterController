using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    [SerializeField]
    float hitPoints = 25; // The enemy's health points

    void Hit(float rawDamage) // This method is called when the enemy takes damage
    {
        hitPoints -= rawDamage; // Reduce the enemy's hit points by the raw damage
        
        if (hitPoints <= 0f)  // If the enemy's hit points are zero or less, schedule self-termination
        {
            Invoke("selfTerminate", 0f);
        }
    }

    void selfTerminate() // This method destroys the enemy game object
    {
        Destroy(gameObject);
    }
}
