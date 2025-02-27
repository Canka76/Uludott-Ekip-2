
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ComputerManager : MonoBehaviour
{
    public static ComputerManager instance;
    
    public Transform sectorsParent;
    public Transform puzzlesParent;

    public bool isPuzzleComplete = false;
    public bool isFocus = false;
    
    private int rng = -1;
    private List<Sector> sectors = new List<Sector>();
    
    void Awake()
    {
        instance = this;
        sectors = new List<Sector>(sectorsParent.GetComponentsInChildren<Sector>());
    }
    
    void Start()
    {
        StartCoroutine(UpdateRngAtRandomIntervals());
    }

    void Update()
    {
        if (rng != -1)
        {
            Sector sector = sectors[rng];
            if (sector != null)
            {
                if(sector.puzzleManager != null && sector.puzzleManager.IsPuzzleComplete())
                {
                    sector.SetIsCompleteState(true);
                    sector.SetSectorStatusText("Stabil");
                    rng = -1;   
                } else {
                    sector.SetIsCompleteState(false);
                    sector.SetSectorStatusText("Stabil Değil");
                }
            }
        }
        
    }
    
    public void IsFocusState(bool state)
    {
        this.isFocus = state;
    }

    public void SetPuzzleCompleteState(bool state)
    {
        isPuzzleComplete = state;
    }
    
    
    private IEnumerator UpdateRngAtRandomIntervals()
    {
        while (true)
        {
            if (rng == -1)
            {
                rng = Random.Range(0, 2);
                Debug.Log("RNG: " + rng);
                sectors[rng].puzzleManager.setIsPuzzleComplete(false);  
            }
            float waitTime = Random.Range(1f, 5f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void A(PuzzleManager puzzleManager)
    {
        puzzleManager.setIsPuzzleComplete(true);
    }
    
}