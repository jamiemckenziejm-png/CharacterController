using UnityEngine;

public class PushObjects : MonoBehaviour
{
    public float pushPower = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        //check if the object has a rigid body and is not kinematic
        if (body == null || body.isKinematic) return;

        //don't push objects below the character 
        if (hit.moveDirection.y < -0.3f) return;

        // Calculate Push Direction (horizontal only)
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // Apply the force to the object
        body.AddForce(pushDir * pushPower, ForceMode.Impulse);
    }

   
}
