using UnityEngine;

public class TestInteractable : Interactable
{
    public override void OnFocus()
    {
        Debug.Log("TestInteractable.OnFocus");
    }

    public override void OnLoseFocus()
    {
        Debug.Log("TestInteractable.OnLoseFocus");
    }

    public override void OnInteract()
    {
        Debug.Log("TestInteractable.OnInteract");
    }
}
