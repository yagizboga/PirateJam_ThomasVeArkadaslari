using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform shooterTransform;
    private Transform coalPlayerTransform;
    private Transform driverPlayerTransform;

    [SerializeField] Animator animator;
    [SerializeField] private float yTolerance = 1.0f;

    private EnemyShoot enemyShoot;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        shooterTransform = GameObject.FindGameObjectWithTag("shooter").transform;
        coalPlayerTransform = GameObject.FindGameObjectWithTag("CoalPlayer").transform;
        driverPlayerTransform = GameObject.FindGameObjectWithTag("DriverPlayer").transform;
        enemyShoot = GetComponent<EnemyShoot>();
    }

    void FixedUpdate()
    {
        Transform target = DetermineTarget();
        if (target != null)
        {
            agent.SetDestination(target.position);
            enemyShoot.SetTarget(target.gameObject);
        }
        else
        {
            Debug.Log("target is null!");
        }
        animator.SetFloat("speed", agent.velocity.magnitude);
    }

    private Transform DetermineTarget()
    {
        float enemyY = transform.position.y;
        Transform closestTarget = null;
        float closestDistance = float.MaxValue;

        /*if (shooterTransform != null && Mathf.Abs(shooterTransform.position.y - enemyY) <= yTolerance)
        {
            float shooterDistance = Vector3.Distance(transform.position, shooterTransform.position);
            float coalDistance = coalPlayerTransform != null ? Vector3.Distance(transform.position, coalPlayerTransform.position) : float.MaxValue;
            float driverDistance = driverPlayerTransform != null ? Vector3.Distance(transform.position, driverPlayerTransform.position) : float.MaxValue;

            if (shooterDistance < coalDistance && shooterDistance < driverDistance)
            {
                //Debug.Log("shooter y pos");
                return shooterTransform;
            }
        }*/

        if (coalPlayerTransform != null)
        {
            float coalDistance = Vector3.Distance(transform.position, coalPlayerTransform.position);
            if (coalDistance < closestDistance)
            {
                closestTarget = coalPlayerTransform;
                closestDistance = coalDistance;
                //Debug.Log("coalPlayer close");
            }
        }

        if (driverPlayerTransform != null)
        {
            float driverDistance = Vector3.Distance(transform.position, driverPlayerTransform.position);
            if (driverDistance < closestDistance)
            {
                closestTarget = driverPlayerTransform;
                closestDistance = driverDistance;
                //Debug.Log("driverPlayer close");
            }
        }

        if (shooterTransform != null)
        {
            float shooterDistance = Vector3.Distance(transform.position, shooterTransform.position);
            if (shooterDistance < closestDistance)
            {
                closestTarget = shooterTransform;
                //Debug.Log("shooterPlayer close");
            }
        }

        return closestTarget;
    }
}
