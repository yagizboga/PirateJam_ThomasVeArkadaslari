using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] int health = 3;
    public void TakeDamage(int a){
        health-=a;
        Debug.Log(name + " " + health);
    }
}
