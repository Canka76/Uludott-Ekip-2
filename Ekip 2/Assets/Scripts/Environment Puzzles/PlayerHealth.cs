
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
    }

    void GameOver()
    {
        if (health <= 0f)
        {
            Debug.LogError("Player is Dead" + health.ToString());
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
