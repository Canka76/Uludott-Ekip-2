using UnityEngine;

public class ComputerInteractable : PuzzleInteract
{
    public override bool OnPuzzleInteract()
    {
        ComputerManager.instance.IsFocusState(true);
        return true;
    }

    public override bool OnPuzzleComplete(bool isPuzzleComp)
    {
        return ComputerManager.instance.isPuzzleComplete;
    }

    public override bool OnPuzzleReset()
    {
        return false;
    }

    public override bool OnPuzzleClose()
    {
        ComputerManager.instance.IsFocusState(false);
        return false;
    }

    public void OnPuzzleUpdate()
    {
        base.OnPuzzleUpdate();
    }
    
}