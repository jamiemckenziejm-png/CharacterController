using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Zone2LockedArea : MonoBehaviour
{

    public int requiredCoins = 3; // Number of coins required to unlock the area
    public GameObject doorObject; // assign the visual barrier object

    private bool isUnlocked = false; // Track if the area is unlocked

    private void OnTriggerEnter(Collider other)
    {
        if(isUnlocked) return; // If already unlocked, do nothing

        Inventory playerInventory = other.GetComponent<Inventory>();
         if(playerInventory != null)
         {
            if(playerInventory.coinCount >= requiredCoins)
            {
                playerInventory.SpendCoins(requiredCoins); // Spend the required coins
                UnlockArea(); // Unlock the area
            }
            else
            {
                Debug.Log("Not enough coins to unlock the area. Current coins: " + playerInventory.coinCount);
            }
        }

    }

    private void UnlockArea()
    {
        // destroy the door object to visually open the area
        if (doorObject != null)
        {
            Destroy(doorObject);
            Debug.Log ("Area unlocked! " + requiredCoins + " coins spent.");
        }
    }


}
