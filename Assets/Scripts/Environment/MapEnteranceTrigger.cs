using UnityEngine;

public class MapEnteranceTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        Debug.Log(other.name);
        //gameObject.transform.parent.gameObject.transform.parent.GetComponent<MapGenerator>().GenerateNextMap();
    }
}
