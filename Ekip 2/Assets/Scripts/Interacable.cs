using System;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
   public void Awake()
   {
      gameObject.layer = 9;
   }

   public abstract void OnFocus();
   public abstract void OnLoseFocus();
   public abstract void OnInteract();
}
