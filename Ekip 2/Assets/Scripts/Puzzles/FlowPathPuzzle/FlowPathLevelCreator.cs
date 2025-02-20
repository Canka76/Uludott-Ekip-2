using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FlowPathLevelCreator : MonoBehaviour
{
    public GameObject linePrefab;  // �izim i�in prefab
    public Transform gridParent;  // Grid alan�
    [Header("Level Settings")]
    public int seed = 0;  // Random seed
    public int minDot = 2;  // En az ka� nokta olmal�
    public int maxDot = 5; // En fazla ka� nokta olmal�

    private List<RectTransform> dots = new List<RectTransform>();
    private LineRenderer lineRenderer;

    void Start()
    {
       
    }


    void Update()
    {
        
    }
}
