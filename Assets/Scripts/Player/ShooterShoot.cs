using Unity.VisualScripting;
using UnityEngine;

public class ShooterShoot : MonoBehaviour
{
    [SerializeField] Camera maincamera;
    [SerializeField] Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            animator.SetTrigger("isShooting");
        }
    }

    public void Shoot(){
        RaycastHit hit;
        Debug.Log("shoot");
        if(Physics.Raycast(maincamera.transform.position,maincamera.transform.forward,out hit)){
            if(hit.collider.CompareTag("enemy")){
                hit.collider.gameObject.GetComponent<HealthScript>().TakeDamage(1);
            }
        }
    }

    public void StopShooting(){
        animator.SetTrigger("isNotShooting");
    }
}
