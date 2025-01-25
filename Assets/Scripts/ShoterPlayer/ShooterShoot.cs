using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterShoot : MonoBehaviour
{
    [SerializeField] Camera maincamera;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem shootingparticle;

    private bool canShoot = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && canShoot)
        {
            animator.SetTrigger("isShooting");
            Shoot();
            shootingparticle.Play();
            canShoot = false;
            StartCoroutine(ShootCoolDown());
        }
    }

    public void Shoot(){
        RaycastHit hit;
        Debug.Log("shoot");
        if(Physics.Raycast(maincamera.transform.position,maincamera.transform.forward,out hit)){
            if(hit.collider.CompareTag("enemy")){
                hit.collider.gameObject.GetComponent<HealthScript>().TakeDamage(1);
                Debug.Log("enemy hit ");
            }
        }
    }

    public void StopShooting(){
        animator.SetTrigger("isNotShooting");
    }

    private IEnumerator ShootCoolDown()
    {
        yield return new WaitForSeconds(0.20f);
        canShoot = true;
    }
}
