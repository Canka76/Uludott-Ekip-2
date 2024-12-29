using System;
using UnityEngine;

public class VerticalMover : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;

    [Header("Boundary Objects")]
    public Transform topBoundary;
    public Transform bottomBoundary;

    private float upperLimit;
    private float lowerLimit;

    private bool canLose = default;

    private int _progress = 50;
    public int Progress
    {
        get => _progress;
        set => _progress = Mathf.Max(0, value); // Prevent progress from going negative
    }

    private float lastTriggerTime = 0f; // Tracks last time the trigger logic was executed
    [SerializeField] private float triggerCooldown = 1f; // Cooldown duration in seconds

    void Start()
    {
        if (topBoundary == null || bottomBoundary == null)
        {
            Debug.LogError("Boundary objects are not assigned!");
            enabled = false;
            return;
        }

        upperLimit = topBoundary.position.y;
        lowerLimit = bottomBoundary.position.y;
        
        InvokeRepeating("DecreaseProgress", 0, triggerCooldown);
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float newYPosition = transform.position.y + verticalInput * speed * Time.deltaTime;
        newYPosition = Mathf.Clamp(newYPosition, lowerLimit, upperLimit);
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

        
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the cooldown has elapsed
        if (Time.time - lastTriggerTime >= triggerCooldown)
        {
            Progress -= 1;
            if (other.CompareTag("Bar/Green"))
            {
                Progress += 15;
                Debug.Log("Progress increased by 4");
            }
            else if (other.CompareTag("Bar/Orange"))
            {
                Progress += 10;
                Debug.Log("Progress increased by 2");
            }

           

            // Update the last trigger time
            lastTriggerTime = Time.time;
        }
    }

    private void DecreaseProgress()
    {
        Progress -= 1;
        Debug.Log($"Current Progress: {Progress}");
    }
}
