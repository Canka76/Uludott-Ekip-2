using System.Collections;
using UnityEngine;

public class PipeDamage : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    public bool shouldFire = false;
    public float damagePerSecond = 10f;
    private bool isPlayerInside = false;
    private GameObject player;

    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        if (_particleSystem == null)
        {
            Debug.LogError("ParticleSystem component is missing!");
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
        if (shouldFire)
        {
            if (!_particleSystem.isPlaying)
            {
                _particleSystem.Play();
            }
        }
        else
        {
            if (_particleSystem.isPlaying)
            {
                _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
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
        // Assuming the player has a script with a method to take damage
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null && shouldFire)
        {
            playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}