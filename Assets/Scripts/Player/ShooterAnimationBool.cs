using UnityEngine;

public class ShooterAnimationBool : MonoBehaviour
{
    [SerializeField] ShooterShoot shooterShoot;

    public void ShootTrigger(){
        shooterShoot.Shoot();
    }
}
