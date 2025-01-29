using UnityEngine;

public class EnemyActivateTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            EnemyMovement enemy = other.gameObject.GetComponent<EnemyMovement>();
            if (enemy != null) 
            {
                enemy.ActivateEnemy();
            }
        }
    }
}
