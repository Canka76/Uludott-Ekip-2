using UnityEngine;

public class CarryExtinguisher : Interactable
{
    public Transform carryPosition; // Assign a child of the camera or moving object
    public bool isCarried = false;
    private Transform originalParent;
    [SerializeField] private float dropForce = 5f;

    private void Update()
    {
        if (isCarried)
        {
            FollowCamera(); // Ensures object follows camera smoothly
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                Drop();
            }
        }
    }

    public override void OnFocus()
    {
        // Highlight the object or show UI hint
    }

    public override void OnLoseFocus()
    {
        // Remove highlight or UI hint
    }

    public override void OnInteract()
    {
        if (!isCarried)
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        originalParent = transform.parent;
        transform.SetParent(carryPosition); // Attach to carry position
        isCarried = true;
        GetComponent<Rigidbody>().isKinematic = true; // Disable physics
    }

    private void Drop()
    {
        transform.SetParent(originalParent); // Restore original parent
        isCarried = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; // Enable physics
        rb.linearVelocity = carryPosition.forward * dropForce; // Apply forward force
    }

    private void FollowCamera()
    {
        // Match carryPosition's position & rotation
        transform.position = carryPosition.position;
        transform.rotation = carryPosition.rotation;
    }
}