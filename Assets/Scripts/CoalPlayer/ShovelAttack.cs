using UnityEngine;

public class ShovelAttack : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private BoxCollider boxCollider;

    private bool canGiveDamage = true;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        canGiveDamage = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("enemy") && canGiveDamage)
        {
            enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(1);
            enemyHealth.PlayShovelBlood();
            canGiveDamage=false;
            boxCollider.enabled = false;
        }
    }

    public void SetCanShovelDamageTrue()
    {
        canGiveDamage = true;
        boxCollider.enabled = true;
    }

    public void SetCanShovelDamageFalse()
    {
        canGiveDamage = false;
        boxCollider.enabled = false;
    }
}
