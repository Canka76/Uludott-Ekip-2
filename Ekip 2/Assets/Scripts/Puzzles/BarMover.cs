using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;
using UnityEngine.UIElements; // Make sure you include this to use DOTween

public class RandomTweenTrigger : MonoBehaviour
{
    [SerializeField] float minTimeForMoveAgain = 1f; // Minimum time between tween triggers
    [SerializeField] float maxTimeForMoveAgain = 2f; // Maximum time between tween triggers
    [SerializeField,Range(0,2)] float minMoveDuration = .5f; // min duration of tween
    [SerializeField,Range(0,3)] float maxMoveDuration = 2f; // Maximum duration of tween

    
    public Transform BarLimit1,BarLimit2;
    void Start()
    {
        // Start the coroutine to trigger the tween
        StartCoroutine(TriggerTweenAtRandomIntervals());
    }

    public IEnumerator TriggerTweenAtRandomIntervals()
    {
        while (true) // Keep running indefinitely
        {
            float randomDelay = Random.Range(minTimeForMoveAgain, maxTimeForMoveAgain); // Get a random time
            yield return new WaitForSeconds(randomDelay); // Wait for the random time
            
            TriggerTween(); // Trigger your tween
        }
    }

    void TriggerTween()
    {
        // Example of a simple tween:
        // Move the GameObject to a random position over a random duration.
        
        Vector3 randomTarget = new Vector3(
            Random.Range(BarLimit1.position.x, BarLimit2.position.x), // Random X position
            Random.Range(BarLimit1.position.y, BarLimit2.position.y), // Random Y position
            Random.Range(BarLimit1.position.z, BarLimit2.position.z) // Random Z position
        );

        float randomDuration = Random.Range(minMoveDuration, maxMoveDuration); // Random duration for the tween

        // Move the object to the random target position
        transform.DOMove(randomTarget, randomDuration).SetEase(Ease.InOutElastic);

        // Optionally, you can also add other tweens (e.g., scaling, rotation, etc.)
    }
}