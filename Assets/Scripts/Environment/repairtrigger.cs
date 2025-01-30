using UnityEngine;

public class repairtrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("trainfront")){
            Debug.Log("trigger!!!");
            GameObject.FindGameObjectWithTag("repairtrigger").GetComponent<RepairTrigger>().SetIsBroken(true);
            gameObject.SetActive(false);
        }
    }
}
