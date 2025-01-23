using UnityEngine;

public class ShooterAnimationEvent : MonoBehaviour
{
    [SerializeField] ShooterShoot shooterShoot;

    public void ShootTrigger(){
        shooterShoot.Shoot();
    }
    public void isNotShootingTrigger(){
        shooterShoot.StopShooting();
    }
}
