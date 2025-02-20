using UnityEngine;

public class Node : MonoBehaviour
{
    public bool isStart;  // Baþlangýç noktasý mý?
    public bool isEnd;    // Bitiþ noktasý mý?

    private void OnMouseDown()
    {
        // Eðer bu bir baþlangýç noktasýysa
        if (isStart)
        {
            Debug.Log("Baþlangýç noktasý seçildi: " + name);
        }

        // Eðer bu bir bitiþ noktasýysa
        if (isEnd)
        {
            Debug.Log("Bitiþ noktasý seçildi: " + name);
        }
    }
}