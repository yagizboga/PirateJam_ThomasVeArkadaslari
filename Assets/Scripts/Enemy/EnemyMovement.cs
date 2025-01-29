using UnityEngine;
using UnityEngine.AI;



public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform shooterTransform;
    private Transform coalPlayerTransform;
    private Transform driverPlayerTransform;


    [SerializeField] Animator animator;

    //y aralalýðýnda shooter varsa ve diger carlardan daha yakýndaysa shootera, yoksa kürekci veya driverdn hangisi yakýnsa ona
    void Awake(){
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        shooterTransform = GameObject.FindGameObjectWithTag("shooter").transform;
        coalPlayerTransform = GameObject.FindGameObjectWithTag("CoalPlayer").transform;
        driverPlayerTransform = GameObject.FindGameObjectWithTag("DriverPlayer").transform;
    }

    void FixedUpdate()
    {
        if(shooterTransform != null)
        {
            agent.SetDestination(shooterTransform.position);
        }
        animator.SetFloat("speed",agent.velocity.magnitude);
    }
}
