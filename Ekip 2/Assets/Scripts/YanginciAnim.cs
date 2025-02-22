using System;
using DG.Tweening;
using UnityEngine;

public class YanginciAnim : MonoBehaviour
{
    private Vector3 startRotation; // Initial rotation in Euler angles
    private Vector3 finalRotation; // Target rotation in Euler angles
    
    [SerializeField] private CarryExtinguisher carryExtinguisher; // Reference to the script

    private bool isFiring = false; // Track the current state

    void Start()
    {
        // Initialize startRotation
        startRotation = new Vector3(15f, 0f, 0f);

        // Initialize finalRotation (if not set in the Inspector)
        if (finalRotation == Vector3.zero) // Check if finalRotation is not set in the Inspector
        {
            finalRotation = new Vector3(0f, 0f, 0f);
        }
        

        // Ensure carryExtinguisher is assigned
        if (carryExtinguisher == null)
        {
            Debug.LogError("CarryExtinguisher reference is missing!");
        }
    }

    void Update()
    {
        // Check if the firing state has changed
        if (carryExtinguisher.isFiring != isFiring)
        {
            isFiring = carryExtinguisher.isFiring;

            // Rotate to the target rotation based on the firing state
            if (isFiring)
            {
                transform.rotation = Quaternion.Euler(startRotation);
            }
            else
            {
               transform.rotation = Quaternion.Euler(finalRotation);
            }
        }
    }
}