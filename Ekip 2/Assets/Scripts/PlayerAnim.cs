using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private float[] desiredTimes;    // Array of times to play the animation
    [SerializeField] private float[] desiredDurations; // Array of durations for each animation

    private Animator animator;
    private int currentIndex = 0; // Tracks the current index in the arrays

    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is missing!");
        }

        // Start the coroutine to play animations
        StartCoroutine(PlayAnimationsAtDesiredTimes());
    }

    IEnumerator PlayAnimationsAtDesiredTimes()
    {
        // Loop through the desiredTimes array
        for (int i = 0; i < desiredTimes.Length; i++)
        {
            // Wait until the desired time is reached
            yield return new WaitForSeconds(desiredTimes[i] - (i > 0 ? desiredTimes[i - 1] : 0));

            // Play the animation for the desired duration
            yield return StartCoroutine(PlayAnimation(desiredDurations[i]));
        }
    }

    IEnumerator PlayAnimation(float duration)
    {
        // Play the animation
        animator.enabled = true;
        //animator.Play("Sallanma");

        // Wait for the desired duration
        yield return new WaitForSeconds(duration);

        // Stop the animation (optional, depending on your needs)
        animator.enabled = false; // Replace "Idle" with the name of your default animation
    }
}