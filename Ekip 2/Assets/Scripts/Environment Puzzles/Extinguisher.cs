using System;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
   [SerializeField] float _speed = 0.4f;

   private CarryExtinguisher carryExtinguisher;

   private void Start()
   {
       carryExtinguisher = GetComponentInParent<CarryExtinguisher>();
   }

   void Update()
    {
        if (carryExtinguisher.isFiring && Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 4f) && hit.collider.TryGetComponent(out ParticleFire particleFire))
        {
            particleFire.TryExtinguishFire(_speed * Time.deltaTime);
        }
    }
}
