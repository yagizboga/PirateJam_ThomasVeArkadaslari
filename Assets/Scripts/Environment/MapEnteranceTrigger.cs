using UnityEngine;

public class MapEnteranceTrigger : MonoBehaviour
{
    [SerializeField] TextAsset dialogue;
    void OnTriggerEnter(Collider other){
        Debug.Log(other.name);
        if(other.CompareTag("train")){
            gameObject.transform.parent.gameObject.transform.parent.GetComponent<MapGenerator>().GenerateNextMap();
        }
        //gameObject.transform.parent.gameObject.transform.parent.GetComponent<MapGenerator>().GenerateNextMap();
    }
}
