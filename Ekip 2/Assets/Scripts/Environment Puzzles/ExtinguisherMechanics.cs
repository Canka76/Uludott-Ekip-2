using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public CarryExtinguisher carryExtinguisher;
    public ParticleSystem extinguisherParticles;
    [SerializeField] private float fireExtinguishTime = 2f; // Time to fully stop fire

    private void Update()
    {
        if (carryExtinguisher.isCarried)
        {
            if (Input.GetMouseButton(0)) // Holding left-click
            {
                extinguisherParticles.Play();
                TryExtinguishFire();
            }
            else
            {
                extinguisherParticles.Stop();
            }
        }
    }

    private void TryExtinguishFire()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f))
        {
            if (hit.collider.CompareTag("Fire"))
            {
                ParticleSystem fireParticles = hit.collider.GetComponent<ParticleSystem>();
                if (fireParticles != null)
                {
                    StartCoroutine(ExtinguishFire(fireParticles));
                }
            }
        }
    }

    private System.Collections.IEnumerator ExtinguishFire(ParticleSystem fireParticles)
    {
        float elapsedTime = 0f;
        ParticleSystem.EmissionModule emission = fireParticles.emission;
        float startRate = emission.rateOverTime.constant;

        while (elapsedTime < fireExtinguishTime)
        {
            elapsedTime += Time.deltaTime;
            float newRate = Mathf.Lerp(startRate, 0, elapsedTime / fireExtinguishTime);
            emission.rateOverTime = newRate;
            yield return null;
        }

        emission.rateOverTime = 0; // Fully stop the fire
        fireParticles.Stop();
    }
}