using UnityEngine;
using UnityEngine.Animations.Rigging;

public class npclookscript : MonoBehaviour
{
    Transform playerhead;
    [SerializeField] MultiAimConstraint multiAimConstraint;
    void Start(){

    }
    void Update(){
        
    }

    void LateUpdate(){
        playerhead = GameObject.FindGameObjectWithTag("DriverNeck").transform;
    }
}
