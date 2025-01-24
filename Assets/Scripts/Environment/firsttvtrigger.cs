using UnityEngine;

public class firsttvtrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Vector")){
            GameObject.FindGameObjectWithTag("IntroManager").GetComponent<IntroManager>().triggerfirsttv();
        }
    }
}