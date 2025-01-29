using UnityEngine;

public class repairtrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("trainfront")){
            GameObject.FindGameObjectWithTag("repairtrigger").GetComponent<RepairTrigger>().SetIsBroken(true);
        }
    }
}
