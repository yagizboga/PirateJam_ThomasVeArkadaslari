using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 3;
    public void TakeDamage(int hit)
    {
        health-=hit;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
