using System.Collections;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShoot : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject currentTarget;

    [SerializeField] private GameObject shooter;
    [SerializeField] private GameObject shooterSpine;
    [SerializeField] private GameObject shooterNeck;

    [SerializeField] private GameObject driverPlayer;
    [SerializeField] private GameObject driverPlayerSpine;
    [SerializeField] private GameObject driverPlayerNeck;

    [SerializeField] private GameObject coalPlayer;
    [SerializeField] private GameObject coalPlayerSpine;
    [SerializeField] private GameObject coalPlayerNeck;

    private PlayerHealth shooterHealth;
    private PlayerHealth driverPlayerHealth;
    private PlayerHealth coalPlayerHealth;


    [SerializeField] GameObject bulletspawnpoint;
    [SerializeField] Animator animator;

    public float shootCooldown = 0.75f;
    private Vector3 direction;
    private Vector3 bodyRotation;
    private bool canShoot = true;

    public float angleFixOffset = -40f;
    public ParticleSystem shootParticle;



    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        shooter = GameObject.FindGameObjectWithTag("Shooter");
        driverPlayer = GameObject.FindGameObjectWithTag("Driver");
        coalPlayer = GameObject.FindGameObjectWithTag("Coal");

        shooterHealth = shooter.GetComponent<PlayerHealth>();
        driverPlayerHealth = driverPlayer.GetComponent<PlayerHealth>();
        coalPlayerHealth = coalPlayer.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if(currentTarget == shooter)
        {
            if (shooterNeck != null)
            {
                direction = (shooterNeck.transform.position - bulletspawnpoint.transform.position).normalized;
            }

            bodyRotation = new Vector3(direction.x, 0, direction.z);
            gameObject.transform.rotation = Quaternion.LookRotation(bodyRotation);

            if (agent.velocity.magnitude < .1f && agent.remainingDistance < agent.stoppingDistance + .1f)
            {
                animator.SetBool("isShooting", true);
                if (canShoot)
                {
                    animator.SetTrigger("shoot");
                    canShoot = false;
                    Shoot();
                    StartCoroutine(ShootCoolDown());
                    //Debug.Log("shot");
                }
            }
            else
            {
                animator.SetBool("isShooting", false);
            }
        }
        else if(currentTarget == driverPlayer)
        {
            if (driverPlayerNeck != null)
            {
                direction = (driverPlayerNeck.transform.position - bulletspawnpoint.transform.position).normalized;
            }

            bodyRotation = new Vector3(direction.x, 0, direction.z);
            gameObject.transform.rotation = Quaternion.LookRotation(bodyRotation);

            if (agent.velocity.magnitude < .1f && agent.remainingDistance < agent.stoppingDistance + .1f)
            {
                animator.SetBool("isShooting", true);
                if (canShoot)
                {
                    animator.SetTrigger("shoot");
                    canShoot = false;
                    Shoot();
                    StartCoroutine(ShootCoolDown());
                    //Debug.Log("shot");
                }
            }
            else
            {
                animator.SetBool("isShooting", false);
            }
        }
        else if(currentTarget == coalPlayer)
        {
            if (coalPlayerNeck != null)
            {
                direction = (coalPlayerNeck.transform.position - bulletspawnpoint.transform.position).normalized;
            }

            bodyRotation = new Vector3(direction.x, 0, direction.z);
            gameObject.transform.rotation = Quaternion.LookRotation(bodyRotation);

            if (agent.velocity.magnitude < .1f && agent.remainingDistance < agent.stoppingDistance + .1f)
            {
                animator.SetBool("isShooting", true);
                if (canShoot)
                {
                    animator.SetTrigger("shoot");
                    canShoot = false;
                    Shoot();
                    StartCoroutine(ShootCoolDown());
                    //Debug.Log("shot");
                }
            }
            else
            {
                animator.SetBool("isShooting", false);
            }
        }
        
    }

    public void Shoot()
    {
        RaycastHit hit;
        Debug.DrawRay(bulletspawnpoint.transform.position, direction, Color.blue);
        if (Physics.Raycast(bulletspawnpoint.transform.position, direction, out hit))
        {
            if (hit.collider.CompareTag("shooter")) 
            {
                if (Random.value <= 0.5f) // %50 hit possibility
                {
                    shooterHealth.TakeDamage(1);
                }
            }
            else if (hit.collider.CompareTag("DriverPlayer"))
            {
                if (Random.value <= 0.5f) // %50 hit possibility
                {
                    driverPlayerHealth.TakeDamage(1);
                }
            }
            else if (hit.collider.CompareTag("CoalPlayer"))
            {
                if (Random.value <= 0.5f) // %50 hit possibility
                {
                    coalPlayerHealth.TakeDamage(1);
                }
            }
        }
        shootParticle.Play();
    }

    private IEnumerator ShootCoolDown()
    {
        yield return new WaitForSeconds(shootCooldown);
        canShoot=true;
    }

    private void LateUpdate()
    {
        if (agent.velocity.magnitude < .1f && agent.remainingDistance < agent.stoppingDistance + .1f)
        {
            if(currentTarget == shooter && shooter != null)
            {
                shooterSpine.transform.LookAt(shooter.transform);
                shooterSpine.transform.rotation = Quaternion.Euler(
                shooterSpine.transform.eulerAngles.x,
                shooterSpine.transform.eulerAngles.y - angleFixOffset,
                shooterSpine.transform.eulerAngles.z
                );
            }
            else if(currentTarget == driverPlayer && driverPlayer != null)
            {
                driverPlayerSpine.transform.LookAt(shooter.transform);
                driverPlayerSpine.transform.rotation = Quaternion.Euler(
                driverPlayerSpine.transform.eulerAngles.x,
                driverPlayerSpine.transform.eulerAngles.y - angleFixOffset,
                driverPlayerSpine.transform.eulerAngles.z
                );
            }
            else if(currentTarget == coalPlayer && coalPlayer != null)
            {
                coalPlayerSpine.transform.LookAt(shooter.transform);
                coalPlayerSpine.transform.rotation = Quaternion.Euler(
                coalPlayerSpine.transform.eulerAngles.x,
                coalPlayerSpine.transform.eulerAngles.y - angleFixOffset,
                coalPlayerSpine.transform.eulerAngles.z
                );
            }
            
        }

    }


    private void OnDrawGizmos()
    {
        if (bulletspawnpoint != null)
        {
            Gizmos.color = Color.red; 
            float rayLength = 100f;  

            if (Physics.Raycast(bulletspawnpoint.transform.position, direction, out RaycastHit hit, rayLength))
            {
                Gizmos.DrawLine(bulletspawnpoint.transform.position, hit.point);
                Gizmos.DrawSphere(hit.point, 0.2f);
            }
            else
            {
                Gizmos.DrawLine(bulletspawnpoint.transform.position, bulletspawnpoint.transform.position + direction * rayLength);
            }
        }
    }
    public void SetTarget(GameObject target)
    {
        currentTarget = target;
    }
}
