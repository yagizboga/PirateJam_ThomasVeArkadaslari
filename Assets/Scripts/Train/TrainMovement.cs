using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+= new Vector3(Time.deltaTime,0,0);
    }
}
