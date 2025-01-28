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
    private float shootCoolDown = 0.162f;

    private int ammoCount = 25;
    private int maxAmmo = 25;
    private bool isReloading = false;

    public GameObject magHand;
    public GameObject hand;
    public GameObject afkRifle;
    public float rotationSpeed = 6f;
    private Transform headTransform;

    private float deltaTime = 0.0f;
    private PlayerMovement playerMovement;
    private GameObject crosshair;


    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        crosshair.SetActive(false);
    }

    void Update()
    {
        if (playerMovement.isActivePlayer)
        {
            crosshair.SetActive(true);
            afkRifle.SetActive(false);
            //DebugFPS();
            //Debug.Log(ammoCount);
            if (Input.GetMouseButton(0) && canShoot && ammoCount > 0)
            {
                animator.SetTrigger("isShooting");
                Shoot();
                shootingparticle.Play();
                recoilCam.ApplyRecoil();
                canShoot = false;
                StartCoroutine(ShootCoolDown());
            }
            if (ammoCount <= 0 && !isReloading)
            {
                Reload();
            }
            if (Input.GetKeyDown(KeyCode.R) && !isReloading && ammoCount < maxAmmo)
            {
                Reload();
            }
        }
        else
        {
            crosshair.SetActive(false);
            afkRifle.SetActive(true);
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(maincamera.transform.position,maincamera.transform.forward,out hit))
        {
            if(hit.collider.CompareTag("enemy"))
            {
                hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
                Instantiate(bloodEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));

                headTransform = null;                                                                   ///////////////////////////////
                foreach (Transform child in hit.collider.transform.GetComponentsInChildren<Transform>())//////
                {                                                                                       ////// HEADSHOT KONTROLU:
                    if (child.gameObject.layer == LayerMask.NameToLayer("Head"))                        //////  
                    {                                                                                   ////// HER HIT ICIN FOREACH YAPIYOR VE TUM CHILD OBJELERI KONTROL EDIYOR
                        headTransform = child;                                                          ////// 
                        break;                                                                          ////// PEFRORMANSI COK KOTU ETKILIYORSA BURAYI KOMPLE SIL
                    }                                                                                   ////// 
                }                                                                                       //////
                Vector3 headPos = new Vector3(headTransform.position.x,                                 //////
                    headTransform.position.y + 0.075f, headTransform.position.z);                       //////
                if (headTransform != null)                                                              //////
                {                                                                                       //////
                    float distanceToHead = Vector3.Distance(hit.point, headPos);                        //////
                    if (distanceToHead <= 0.12f)                                                        //////
                    {                                                                                   //////
                        hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(2);              //////
                        Instantiate(bloodEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal)); //////
                        Instantiate(bloodEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal)); //////
                        //Debug.Log("HEADSHOT");                                                        //////
                    }                                                                                   //////
                }                                                                                       ///////////////////////////////
            }


            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ragdoll"))
            {
                Instantiate(bloodEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
        ammoCount -= 1;
    }

    public void StopShooting(){
        animator.SetTrigger("isNotShooting");
    }

    private IEnumerator ShootCoolDown()
    {
        yield return new WaitForSeconds(shootCoolDown);
        if (!isReloading)
        {
            canShoot = true;
        }
    }

    /*private IEnumerator ReloadTime()
    {
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
        ammoCount = maxAmmo;
        isReloading = false;
    }*/

    private void Reload()
    {
        canShoot = false;
        isReloading = true;
        StartCoroutine(ReloadAnim());
    }
    IEnumerator ReloadAnim()
    {
        isReloading = true;

        yield return MoveToPositionY(hand, -0.1f);

        yield return RotateToAngle(magHand, -30f);

        yield return new WaitForSeconds(0.5f);

        yield return RotateToAngle(magHand, 0f);

        yield return MoveToPositionY(hand, 0f);

        canShoot = true;
        ammoCount = maxAmmo;
        isReloading = false;
    }

    IEnumerator RotateToAngle(GameObject obj, float targetAngle)
    {
        Quaternion initialRotation = obj.transform.localRotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        while (Quaternion.Angle(obj.transform.localRotation, targetRotation) > 1.5f)
        {
            obj.transform.localRotation = Quaternion.Lerp(obj.transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        obj.transform.localRotation = targetRotation;
    }

    IEnumerator MoveToPositionY(GameObject obj, float targetY)
    {
        Vector3 initialPosition = obj.transform.localPosition;
        Vector3 targetPosition = new Vector3(initialPosition.x, targetY, initialPosition.z);

        while (Mathf.Abs(obj.transform.localPosition.y - targetY) > 0.008f)
        {
            obj.transform.localPosition = Vector3.Lerp(obj.transform.localPosition, targetPosition, rotationSpeed * 1.5f * Time.deltaTime);
            yield return null;
        }

        obj.transform.localPosition = targetPosition;
    }

    private void DebugFPS()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        Debug.Log("FPS: " + Mathf.Ceil(fps));
    }

}
