using UnityEngine;

public class ChairInteract : Interactable
{
    public override void OnFocus()
    {
        // create the text that will be displayed when the player looks at the chair
        string text = "Press "+ FpsController.instance.interactKey + " to sit";
        UIManager.instance.ShowInteractText(text);
    }
    
    public override void OnLoseFocus()
    {
        UIManager.instance.HideInteractText();
    }

    public override void OnInteract()
    {
        if (Input.GetKeyDown(FpsController.instance.interactKey))
        {
            FpsController.instance.Sit();
        }
    }
}
