using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class FlowPathPuzzleController : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject linePrefab;
    public Transform gridParent;
    public int gridSize = 3;
    public List<Vector3> correctPath; // Doðru yol (3D koordinatlar)

    private List<Vector3> currentPath = new List<Vector3>(); // Oyuncunun çizdiði yol
    private GameObject currentLine;
    private LineRenderer lineRenderer;
    private Dictionary<Vector3, RectTransform> cellMap = new Dictionary<Vector3, RectTransform>();

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        foreach (Button button in gridParent.GetComponentsInChildren<Button>())
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => Debug.Log(button.name));
        }
    }

    public void OnCellClick(Vector3 position)
    {
        Debug.Log("Týklanan hücre: " + position);

        if (currentPath.Count == 0)
        {
            StartNewLine(cellMap[position].position);
        }

        if (!currentPath.Contains(cellMap[position].position))
        {
            currentPath.Add(cellMap[position].position);
            UpdateLine(cellMap[position]);

            if (currentPath.Count == correctPath.Count)
            {
                CheckPath();
            }
        }
    }

    void StartNewLine(Vector3 start)
    {
        currentPath.Add(start);
        currentLine = Instantiate(linePrefab, gridParent);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, start);
        lineRenderer.useWorldSpace = true;
    }

    void UpdateLine(RectTransform next)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, next.position);
    }

    void CheckPath()
    {
        for (int i = 0; i < correctPath.Count; i++)
        {
            if (currentPath[i] != correctPath[i])
            {
                Debug.Log("Yanlýþ yol çizildi!");
                ResetPuzzle();
                return;
            }
        }

        Debug.Log("Tebrikler! Doðru yolu çizdiniz!");
        ResetPuzzle();
    }

    void ResetPuzzle()
    {
        currentPath.Clear();
        if (currentLine != null)
        {
            Destroy(currentLine);
        }
    }
}
