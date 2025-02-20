using UnityEngine;

public class Node : MonoBehaviour
{
    public bool isStart;  // Ba�lang�� noktas� m�?
    public bool isEnd;    // Biti� noktas� m�?

    private void OnMouseDown()
    {
        // E�er bu bir ba�lang�� noktas�ysa
        if (isStart)
        {
            Debug.Log("Ba�lang�� noktas� se�ildi: " + name);
        }

        // E�er bu bir biti� noktas�ysa
        if (isEnd)
        {
            Debug.Log("Biti� noktas� se�ildi: " + name);
        }
    }
}