
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
// qUİCK tİME eVENT
public class QTEInteractable : PuzzleInteract
{
    [Header("Component")]
    public Transform puzzleTransform;
    
    public override bool OnPuzzleInteract()
    {
        QTEPuzzleManager.instance.IsFocusState(true);
        return true;
    }

    public override bool OnPuzzleComplete(bool isPuzzleComp)
    {
        return Mathf.Approximately(QTEPuzzleManager.instance.getProcess(), 100f);
    }

    public override bool OnPuzzleReset()
    {
        
        return false;

    }

    public override bool OnPuzzleClose()
    {
        QTEPuzzleManager.instance.IsFocusState(false);
        return false;

    }
    
    public void OnPuzzleUpdate()
    {
        base.OnPuzzleUpdate();
    }
}
