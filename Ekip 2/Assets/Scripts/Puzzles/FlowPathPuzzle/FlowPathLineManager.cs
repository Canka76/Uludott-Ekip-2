using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FlowPathPuzzleController))]
public class FlowPathLineManager : MonoBehaviour
{
    public GameObject linePrefab;      // Çizgi prefab'ý
    public int gridSize = 4;           // Izgara boyutu
    public Transform gridParent;       // Izgara parent'ý

    private FlowPathPuzzleController puzzleController;
    private GameObject[,] nodes;       // Noktalarýn referanslarýný saklama

    void Start()
    {
        puzzleController = GetComponent<FlowPathPuzzleController>();

        linePrefab = puzzleController.linePrefab;
        gridSize = puzzleController.gridSize;
        gridParent = puzzleController.gridParent;

        nodes = new GameObject[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                nodes[x, y] = gridParent.GetChild(x * gridSize + y).gameObject;
            }
        }

        DrawLines();
    }

    void DrawLines()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if (x < gridSize - 1) DrawLine(nodes[x, y], nodes[x + 1, y]); // Sað baðlantý
                if (y < gridSize - 1) DrawLine(nodes[x, y], nodes[x, y + 1]); // Yukarý baðlantý
            }
        }
    }

    void DrawLine(GameObject nodeA, GameObject nodeB)
    {
        GameObject line = Instantiate(linePrefab, parent: gridParent.parent);
        LineRenderer lr = line.GetComponent<LineRenderer>();
        lr.useWorldSpace = true;
        lr.SetPosition(0, nodeA.transform.position);
        lr.SetPosition(1, nodeB.transform.position);
    }
}