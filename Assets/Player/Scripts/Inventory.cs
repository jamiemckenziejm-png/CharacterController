using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;

public class Inventory : MonoBehaviour
{
    // Track the number of coins the player has collected
    public int coinCount = 0;

    // Singleton instance of the Inventory class
    public static Inventory Instance;

    private void Awake()
    {
        // Ensure that only one instance of the Inventory class exists
        if (Instance == null)
        {
            Instance = this; // Set the instance to this object
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed when loading new scenes
        }
        else
        {
            Destroy(gameObject); // Destroy this object if another instance already exists
        }
    }

    // Call this method to add coins to the player's inventory
    public void AddCoins(int amount = 1)
    {
        coinCount += amount;
        Debug.Log("Coin collected: " + coinCount);
    }

    // Call this method to spend/use coins
    public bool SpendCoins(int amount = 1)
    {
        if (coinCount >= amount)
        {
            coinCount -= amount;
            Debug.Log("Coins spent: " + amount + ". Remaining coins: " + coinCount);
            return true; // Return true if the coins were successfully spent
        }
        else
        {
            Debug.Log("Not enough coins to spend. Current coins: " + coinCount);
            return false; // Return false if there are not enough coins to spend
        }
    }
}

