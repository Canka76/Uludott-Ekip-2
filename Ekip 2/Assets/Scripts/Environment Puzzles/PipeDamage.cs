using UnityEngine;

public class PipeDamage : MonoBehaviour
{
    public ParticleSystem _particleSystem;
    public bool shouldFire = false;
    public float damagePerSecond = 10f;

    private bool isPlayerInside = false;
    private GameObject player;

    void Start()
    {
        // Find the ParticleSystem in the children
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        if (_particleSystem == null)
        {
            Debug.LogError("No ParticleSystem found in children of " + gameObject.name);
        }
    }

    void Update()
    {
        if (isPlayerInside && player != null)
        {
            DamagePlayer();
        }

        ParticleController();
    }

    void ParticleController()
    {
        if (_particleSystem == null) return;

        if (shouldFire && !_particleSystem.isPlaying)
        {
            _particleSystem.Play();
        }
        else if (!shouldFire && _particleSystem.isPlaying)
        {
            _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            player = null;
        }
    }

    private void DamagePlayer()
    {
        if (!shouldFire || player == null) return;

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}