using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 10;
    private int health;

    private void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(int hit)
    {
        health -= hit;
        Debug.Log(health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
