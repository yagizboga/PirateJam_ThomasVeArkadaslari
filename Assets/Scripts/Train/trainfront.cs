using UnityEngine;

public class trainfront : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("repairtrigger")){
            GetComponent<AudioSource>().Play();
        }
    }
}
