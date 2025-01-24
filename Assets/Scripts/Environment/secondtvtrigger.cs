using UnityEngine;

public class secondtvtrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        GameObject.FindGameObjectWithTag("IntroManager").GetComponent<IntroManager>().triggersecondtv();
    }
}
