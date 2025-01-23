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
    [SerializeField] GameObject head;
    [SerializeField] GameObject arm1;
    [SerializeField] GameObject arm2;
    float cooldown = 2f;
    Vector3 direction;
    Vector3 bodyRotation;
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
        bodyRotation = new Vector3(direction.x,0,direction.z);
        gameObject.transform.rotation =  Quaternion.LookRotation(bodyRotation);  
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
