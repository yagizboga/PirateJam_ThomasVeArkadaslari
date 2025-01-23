using UnityEngine;

public class EnemyAnimatorEvent : MonoBehaviour
{
    [SerializeField] EnemyShoot enemyShoot;
    public void isShooting(){
        enemyShoot.Shoot();
    }
}
