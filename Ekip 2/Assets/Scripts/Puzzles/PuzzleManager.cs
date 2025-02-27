
using UnityEngine;

public abstract class PuzzleManager : MonoBehaviour
{
    protected bool isPuzzleComplete { get; set; }
    public abstract bool IsPuzzleComplete();
    
    public void setIsPuzzleComplete(bool status)
    {
        isPuzzleComplete = status;
    }
}
