using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShoot : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject shooter;
    [SerializeField] GameObject bulletspawnpoint;
    [SerializeField] Animator animator;
    float cooldown = 2f;
    Vector3 direction;
    void Awake(){
        agent = GetComponent<NavMeshAgent>();
        

    }
    void Start()
    {
        shooter = GameObject.FindGameObjectWithTag("shooter");
    }

    // Update is called once per frame
    void Update()
    {
        direction = (shooter.transform.position - bulletspawnpoint.transform.position).normalized; 
        gameObject.transform.rotation =  Quaternion.LookRotation(direction);
        if(agent.velocity.magnitude < .1f && agent.remainingDistance < agent.stoppingDistance +.1f){
            animator.SetBool("isShooting",true);
        }
        else{
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
}
