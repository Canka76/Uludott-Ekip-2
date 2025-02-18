using UnityEngine;

public class Extinguisher : MonoBehaviour
{
   [SerializeField] float _speed = 0.4f;

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 4f) && hit.collider.TryGetComponent(out ParticleFire particleFire))
        {
            particleFire.TryExtinguishFire(_speed * Time.deltaTime);
        }
    }
}
