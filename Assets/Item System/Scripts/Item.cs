using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Item : MonoBehaviour
{
    public GameObject itemPrefab; // Prefab for the item
    public Sprite icon; // Icon for the item

    public string itemName; // Name of the item
    [TextArea(4,16)]
    public string description; // Description of the item

    public float weight = 0; // Weight of the item, can be used for inventory management
    public int quantity = 1; // Quantity of the item, useful for stackable items
    public int maxStackableQuantity = 1; // Maximum quantity for stackable items, set to 1 for non-stackable items

    public bool isStorable = false; // Indicates if the item can be stored in an inventory
    public bool isConsumable = true; // Indicates if the item can be consumed (e.g., health potion) 



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact() // Method for interaction with items
    {
        Debug.Log("picked up " + transform.name);

        if (isStorable) // if the item can be stored call the store method 
        {
            Store();
        }
        else // otherwise call the use method  
        {
            Use();
        }
    }

    void Store() // method for storing items 
    {
        Debug.Log("Storing " + transform.name);
        // TODO
        Destroy(gameObject);
    }

    void Use() // method for using items 
    {
        if (isConsumable)
        {
            quantity--;
            if (quantity <=0)
            {
                Destroy(gameObject);
            }
        }
    }
}
