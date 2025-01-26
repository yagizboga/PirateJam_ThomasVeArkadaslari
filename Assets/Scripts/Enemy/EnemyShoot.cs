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

    public float cooldown = 0.75f;
    private Vector3 direction;
    private Vector3 bodyRotation;
    private bool canShoot = true;
    public GameObject spine;
    public float angleFixOffset = -40f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        shooter = GameObject.FindGameObjectWithTag("shooter");
    }

    void Update()
    {
        direction = (shooter.transform.position - bulletspawnpoint.transform.position).normalized; 
        bodyRotation = new Vector3(direction.x,0,direction.z);
        gameObject.transform.rotation =  Quaternion.LookRotation(bodyRotation); 
        
        if(agent.velocity.magnitude < .1f && agent.remainingDistance < agent.stoppingDistance +.1f)
        {
            animator.SetBool("isShooting",true);
            if (canShoot)
            {
                animator.SetTrigger("shoot");
                canShoot = false; 
                StartCoroutine(ShootCoolDown());
            }
        }
        else
        {
            animator.SetBool("isShooting",false);
        }
    }

    public void Shoot(){
        RaycastHit hit;
        Debug.DrawRay(bulletspawnpoint.transform.position,direction,Color.blue);
        if(Physics.Raycast(bulletspawnpoint.transform.position,direction, out hit)){
            if(hit.collider.CompareTag("shooter")){
                Debug.Log("hit!");
            }
        }
    }

    private IEnumerator ShootCoolDown()
    {
        yield return new WaitForSeconds(cooldown);
        canShoot=true;
    }

    private void LateUpdate()
    {
        if (agent.velocity.magnitude < .1f && agent.remainingDistance < agent.stoppingDistance + .1f)
        {
            spine.transform.LookAt(shooter.transform);
            spine.transform.rotation = Quaternion.Euler(
            spine.transform.eulerAngles.x,
            spine.transform.eulerAngles.y - angleFixOffset, // Y ekseninde 25 derece sola çevir
            spine.transform.eulerAngles.z
            );
        }

    }

}
