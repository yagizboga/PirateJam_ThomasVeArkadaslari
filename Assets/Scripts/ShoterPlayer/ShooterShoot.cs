using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterShoot : MonoBehaviour
{
    [SerializeField] Camera maincamera;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem shootingparticle;
    [SerializeField] GameObject bloodEffectPrefab;

    private bool canShoot = true;
    public ShooterCam recoilCam;
    public float shootCoolDown = 0.162f;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && canShoot)
        {
            animator.SetTrigger("isShooting");
            Shoot();
            shootingparticle.Play();
            recoilCam.ApplyRecoil();
            canShoot = false;
            StartCoroutine(ShootCoolDown());
        }
    }

    public void Shoot(){
        RaycastHit hit;
        //Debug.Log("shoot");
        if(Physics.Raycast(maincamera.transform.position,maincamera.transform.forward,out hit)){
            if(hit.collider.CompareTag("enemy")){
                hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
                Instantiate(bloodEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                //Debug.Log("enemy hit ");
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ragdoll"))
            {
                Instantiate(bloodEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }

    public void StopShooting(){
        animator.SetTrigger("isNotShooting");
    }

    private IEnumerator ShootCoolDown()
    {
        yield return new WaitForSeconds(shootCoolDown);
        canShoot = true;
    }
}
