using System;
using UnityEngine;

public class Sector : MonoBehaviour
{
    public int sectorId;
    public PuzzleManager puzzleManager;
    
    private bool isComplete = false;
    private TMPro.TextMeshProUGUI sectorStatusText;
    void Start()
    {
        sectorStatusText = GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1];
    }
    
    public void SetIsCompleteState(bool state)
    {
        isComplete = state;
    }
    
    public bool getIsCompleteState()
    {
        return isComplete;
    }
    
    public String GetSectorStatusText()
    {
        if (sectorStatusText != null)
        {
            return sectorStatusText.text;
        }
        return "";
    }
    
    public void SetSectorStatusText(string text)
    {
        if (sectorStatusText != null)
        {
            sectorStatusText.text = text;
        }
    }
}
