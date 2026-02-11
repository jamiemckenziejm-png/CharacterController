using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Interactor : MonoBehaviour
{
    Transform actorCamera;
    LayerMask layerMask;

    [SerializeField] private float maxDistanceFromCamera = 10f; // Maximum distance for interaction

    [SerializeField] private float maxInteractableDistance = 3f; // Maximum distance for interaction
    private float distancveFromActor;

    PlayerInput playerInput;
    InputAction interactAction;

    void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
        var map = playerInput.currentActionMap;
        interactAction = map.FindAction("Interact", true);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerMask = ~LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer));
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    public void Interact()
    {
        if (interactAction.IsPressed())
        {
            actorCamera = Camera.main.transform;
            Debug.Log("Live Camera: " + actorCamera.name);

            Ray ray = new Ray(actorCamera.position, actorCamera.forward);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, maxDistanceFromCamera, layerMask))
            {
                if (raycastHit.transform != null)
                {
                    distancveFromActor = Vector3.Distance(transform.position, raycastHit.transform.position);
                    if (distancveFromActor <= maxInteractableDistance)
                    {
                        Debug.Log("In Range: " + raycastHit.transform.name + " (" + distancveFromActor.ToString("0.00") + " units)");
                        Item item = raycastHit.transform.GetComponent<Item>();
                        if (item != null)
                        {
                            item.Interact();
                        }
                    }
                }
                //raycastHit
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxInteractableDistance);
    }
}
