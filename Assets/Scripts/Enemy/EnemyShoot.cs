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
    float cooldown = 2f;
    Vector3 direction;
    void Awake(){
        agent = GetComponent<NavMeshAgent>();
        

    }
    void Start()
    {
        shooter = GameObject.FindGameObjectWithTag("shooter");
        StartCoroutine(ShootingLoop());
    }

    // Update is called once per frame
    void Update()
    {
        direction = (shooter.transform.position - bulletspawnpoint.transform.position).normalized; 
    }

    void Shoot(){
        RaycastHit hit;
        Debug.DrawRay(bulletspawnpoint.transform.position,direction,Color.blue);
        if(Physics.Raycast(bulletspawnpoint.transform.position,direction, out hit)){
            if(hit.collider.CompareTag("shooter")){
                Debug.Log("hit!");
            }
        }
    }

    IEnumerator ShootingLoop(){
        //shootingi animasyon evenete ekle buradan animasyon settrigger shoot true ayarla.
        while(true){
            if((shooter.transform.position - transform.position).magnitude <= agent.stoppingDistance + 0.1f){
                Debug.Log("stopped");
                Shoot();
                
            }
            yield return new WaitForSeconds(cooldown);
        }

    }
}
