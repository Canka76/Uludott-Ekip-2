using UnityEngine;

public class CraneController : MonoBehaviour
{
    public float moveSpeed = 2f;              // Crane base movement speed
    public float rotateSpeed = 50f;           // Crane rotation speed
    public Transform hook;                   // Reference to the hook
    public float hookSpeed = 1f;              // Speed of hook movement
    public KeyCode grabKey = KeyCode.Space;   // Key to grab/release rods

    private bool isGrabbing = false;          // Tracks if currently grabbing a rod
    private GameObject grabbedRod;            // Reference to the grabbed rod
    private HingeJoint rodHingeJoint;         // Hinge Joint for the grabbed rod

    private Rigidbody craneRb;                // Rigidbody of the crane
    private Rigidbody hookRb;                 // Rigidbody of the hook

    void Start()
    {
        // Get the Rigidbody component of the crane
        craneRb = GetComponent<Rigidbody>();
        if (craneRb == null)
        {
            Debug.LogError("Crane Rigidbody component is missing!");
        }

        // Ensure the hook has a Rigidbody
        hookRb = hook.GetComponent<Rigidbody>();
        if (hookRb == null)
        {
            Debug.LogError("Hook Rigidbody component is missing!");
        }

        // Add a Hinge Joint to the hook to allow swinging
        HingeJoint hookHingeJoint = hook.gameObject.AddComponent<HingeJoint>();
        hookHingeJoint.connectedBody = craneRb; // Connect to the crane
        hookHingeJoint.anchor = Vector3.zero;   // Anchor at the hook's center
        hookHingeJoint.axis = Vector3.forward;  // Allow rotation around the Z-axis
    }

    void Update()
    {
        HandleMovement();
        HandleHook();
        HandleGrab();
    }

    void HandleMovement()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.A)) horizontal = -1f; // Left
        if (Input.GetKey(KeyCode.D)) horizontal = 1f;  // Right
        if (Input.GetKey(KeyCode.W)) vertical = 1f;    // Forward
        if (Input.GetKey(KeyCode.S)) vertical = -1f;   // Backward

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed;

        // Apply movement to the crane's Rigidbody
        if (craneRb != null)
        {
            craneRb.linearVelocity = movement;
        }
        else
        {
            // Fallback to Translate if Rigidbody is missing
            transform.Translate(movement * Time.deltaTime);
        }
    }

    void HandleHook()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            hook.Translate(Vector3.up * hookSpeed * Time.deltaTime);    // Move hook up
        else if (Input.GetKey(KeyCode.DownArrow))
            hook.Translate(Vector3.down * hookSpeed * Time.deltaTime);  // Move hook down
    }

    void HandleGrab()
    {
        if (Input.GetKeyDown(grabKey))
        {
            if (!isGrabbing)
            {
                // Attempt to grab a rod
                Collider[] hitColliders = Physics.OverlapSphere(hook.position, 0.5f);
                foreach (var hit in hitColliders)
                {
                    if (hit.CompareTag("Rod"))
                    {
                        grabbedRod = hit.gameObject;
                        Rigidbody rodRb = grabbedRod.GetComponent<Rigidbody>();

                        if (rodRb != null)
                        {
                            // Create a Hinge Joint to connect the rod to the hook
                            rodHingeJoint = grabbedRod.AddComponent<HingeJoint>();
                            rodHingeJoint.connectedBody = hookRb; // Connect to the hook
                            rodHingeJoint.anchor = Vector3.zero;  // Anchor at the rod's center
                            rodHingeJoint.axis = Vector3.forward; // Allow rotation around the Z-axis

                            isGrabbing = true; // Set grabbing state
                        }
                        break;
                    }
                }
            }
            else
            {
                // Release the rod
                if (rodHingeJoint != null)
                {
                    Destroy(rodHingeJoint); // Remove the Hinge Joint
                }

                grabbedRod = null; // Clear reference
                isGrabbing = false; // Reset grabbing state
            }
        }
    }
}