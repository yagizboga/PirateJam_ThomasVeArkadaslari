using UnityEngine;
using UnityEngine.AI;



public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform shooterTransform;
    private Transform coalPlayerTransform;
    private Transform driverPlayerTransform;


    [SerializeField] Animator animator;

    //y aralal���nda shooter varsa ve diger carlardan daha yak�ndaysa shootera, yoksa k�rekci veya driverdn hangisi yak�nsa ona
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
