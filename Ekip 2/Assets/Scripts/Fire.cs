using UnityEngine;

public class Fire : MonoBehaviour
{
    // Variables
    public ParticleSystem extinguishingParticles; // Particle system for extinguishing effect
    public Transform carryPosition;              // Position where the extinguisher is carried
    public KeyCode useKey = KeyCode.Mouse0;      // Key to use the extinguisher
    public KeyCode dropKey = KeyCode.Q;          // Key to drop the extinguisher

    private bool isBeingCarried = false;         // Whether the extinguisher is being carried
    private Transform originalParent;           // Original parent for resetting
    private Rigidbody rb;                        // Rigidbody for the extinguisher

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (extinguishingParticles == null)
        {
            Debug.LogError("ExtinguishingParticles not assigned in inspector.");
        }
    }

    void Update()
    {
        if (isBeingCarried)
        {
            CarryActions();
        }
    }

    void CarryActions()
    {
        // Use extinguisher
        if (Input.GetKeyDown(useKey))
        {
            StartExtinguishing();
        }

        if (Input.GetKeyUp(useKey))
        {
            StopExtinguishing();
        }

        // Drop extinguisher
        if (Input.GetKeyDown(dropKey))
        {
            DropExtinguisher();
        }
    }

    public void PickUpExtinguisher()
    {
        isBeingCarried = true;
        rb.isKinematic = true;
        originalParent = transform.parent;
        transform.parent = carryPosition;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void DropExtinguisher()
    {
        isBeingCarried = false;
        transform.parent = originalParent;
        rb.isKinematic = false;
    }

    void StartExtinguishing()
    {
        if (extinguishingParticles != null && !extinguishingParticles.isPlaying)
        {
            extinguishingParticles.Play();
        }
    }

    void StopExtinguishing()
    {
        if (extinguishingParticles != null && extinguishingParticles.isPlaying)
        {
            extinguishingParticles.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Pick up extinguisher when interacting (e.g., player enters trigger zone and presses E)
        if (!isBeingCarried && other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpExtinguisher();
            }
        }
    }
}
