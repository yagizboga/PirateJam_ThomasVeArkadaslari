using UnityEngine;
using UnityEngine.AI;



public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform shooter;
    [SerializeField] Animator animator;
    Rigidbody rb;


    void Awake(){
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        shooter = GameObject.FindGameObjectWithTag("shooter").transform;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(shooter.position);
        if(rb.linearVelocity.magnitude >= 0){
            animator.SetFloat("speed",rb.linearVelocity.magnitude);
        }
    }
}
