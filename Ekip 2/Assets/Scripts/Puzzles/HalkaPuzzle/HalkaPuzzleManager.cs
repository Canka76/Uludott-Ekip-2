using System.Collections.Generic;
using UnityEngine; 

public class HalkaPuzzleManager : PuzzleManager
{
    public Transform halka;
    public Transform levelTransform;
    public ComputerInteractable computerInteractable;
    
    [Header("Prefabs")]
    public GameObject linePrefab;
    
    private enum HalkaPathType
    {
        Duz,
        Yukari,
        Asagi
    }
    
    private void Start()
    {
        LevelGenerate();
    }

    void Update()
    {
        if (ComputerManager.instance.isFocus)
        {
            float d = 0.5f; 
            Vector3 pos = halka.position;
            pos.y = Mathf.Clamp(pos.y + Input.GetAxis("Mouse Y") * Time.deltaTime * 10, transform.position.y - d, transform.position.y + d);
            halka.position = pos;
        }
    }

    public override bool IsPuzzleComplete()
    {
        return isPuzzleComplete;
    }

    public void LevelGenerate(int pointCount = 10)
    {
        
    }

}
