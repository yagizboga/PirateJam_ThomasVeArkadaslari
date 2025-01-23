using UnityEngine;
using UnityEngine.AI;



public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform shooter;
    [SerializeField] Animator animator;


    void Awake(){
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        shooter = GameObject.FindGameObjectWithTag("shooter").transform;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(shooter.position);
        animator.SetFloat("speed",agent.velocity.magnitude);
    }
}
