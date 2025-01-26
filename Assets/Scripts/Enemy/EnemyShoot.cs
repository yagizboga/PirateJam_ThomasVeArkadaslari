using System.Collections;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShoot : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject shooter;
    [SerializeField] GameObject bulletspawnpoint;
    [SerializeField] Animator animator;
    private GameObject shootPositionPlayerNeck;

    public float shootCooldown = 0.75f;
    private Vector3 direction;
    private Vector3 bodyRotation;
    private bool canShoot = true;
    public GameObject spine;
    public float angleFixOffset = -40f;
    public ParticleSystem shootParticle;
    private PlayerHealth shooterHealth;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        shooter = GameObject.FindGameObjectWithTag("shooter");
        shooterHealth = shooter.GetComponent<PlayerHealth>();
        shootPositionPlayerNeck = GameObject.FindGameObjectWithTag("PlayerTakeHitPosition");
    }

    void Update()
    {
        direction = (shootPositionPlayerNeck.transform.position - bulletspawnpoint.transform.position).normalized; 
        bodyRotation = new Vector3(direction.x,0,direction.z);
        gameObject.transform.rotation =  Quaternion.LookRotation(bodyRotation); 
        
        if(agent.velocity.magnitude < .1f && agent.remainingDistance < agent.stoppingDistance +.1f)
        {
            animator.SetBool("isShooting",true);
            if (canShoot)
            {
                animator.SetTrigger("shoot");
                canShoot = false;
                Shoot();
                StartCoroutine(ShootCoolDown());
            }
        }
        else
        {
            animator.SetBool("isShooting",false);
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
                    //Debug.Log("hit!" + ++hittest); 

                }
                else
                {
                    //Debug.Log("miss!");
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
            spine.transform.LookAt(shooter.transform);
            spine.transform.rotation = Quaternion.Euler(
            spine.transform.eulerAngles.x,
            spine.transform.eulerAngles.y - angleFixOffset, 
            spine.transform.eulerAngles.z
            );
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


}
