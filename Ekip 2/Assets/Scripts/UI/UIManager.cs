
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    [Header("UI Elements")]
    [SerializeField] private TMPro.TextMeshProUGUI interactTextMesh;
    
    void Awake()
    {
        Debug.Log("UIManager Awake");
        instance = this;
    }

    public void ShowInteractText(string text)
    {
        interactTextMesh.text = text;
        interactTextMesh.gameObject.SetActive(true);
    }

    public void HideInteractText()
    {
        interactTextMesh.gameObject.SetActive(false);
    }
}