using System;
using UnityEngine;

public class CubeInteract : PuzzleInteract
{

    public override bool OnPuzzleInteract()
    {
        Debug.Log("Puzzle Interacted");
        return true;
    }

    public override bool OnPuzzleComplete(bool isPuzzleComp)
    {
        if (isPuzzleComp)
        {
            Debug.Log("Puzzle Complete");
        }
        return false;
    }

    public override bool OnPuzzleReset()
    {
        Debug.Log("Puzzle Reset");
        return false;

    }

    public override bool OnPuzzleClose()
    {
        Debug.Log("Puzzle Closed");
        return false;

    }
}
