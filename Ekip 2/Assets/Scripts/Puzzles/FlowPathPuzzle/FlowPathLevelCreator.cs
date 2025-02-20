using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FlowPathLevelCreator : MonoBehaviour
{
    public GameObject linePrefab;  // Çizim için prefab
    public Transform gridParent;  // Grid alaný
    [Header("Level Settings")]
    public int seed = 0;  // Random seed
    public int minDot = 2;  // En az kaç nokta olmalý
    public int maxDot = 5; // En fazla kaç nokta olmalý

    private List<RectTransform> dots = new List<RectTransform>();
    private LineRenderer lineRenderer;

    void Start()
    {
       
    }


    void Update()
    {
        
    }
}
