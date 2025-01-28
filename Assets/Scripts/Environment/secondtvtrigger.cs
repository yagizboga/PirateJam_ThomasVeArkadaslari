using UnityEngine;

public class secondtvtrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Vector")){
            GameObject.FindGameObjectWithTag("IntroManager").GetComponent<IntroManager>().triggersecondtv();
        }
    }
}
