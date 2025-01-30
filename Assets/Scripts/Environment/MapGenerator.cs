using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Threading;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField] List<GameObject> maps;
    List<GameObject> activemaps;
    Grid grid;
    int count = 1;

    private float speedInit = 0f;
    private float acceleration = 10f;
    private float finalSpeed;
    void Start(){
        activemaps = new List<GameObject>();
        grid = new Grid(2,1,1000);
        activemaps.Add(Instantiate(maps[0],grid.GetMap(1,0),Quaternion.identity,this.transform));
    }

    public void GenerateNextMap(){
        activemaps.Add(Instantiate(maps[count],grid.GetMap(0,0),Quaternion.identity,this.transform));
        count+=1;
    }

    void FixedUpdate(){
        if(Input.GetKey("o") && GameObject.FindGameObjectWithTag("repairtrigger").GetComponent<RepairTrigger>().isBroken ==false){
            if(speedInit <5){
                speedInit+=Time.deltaTime;
            }
        }
        else{
            if(speedInit > 0){
                speedInit-=Time.deltaTime;
            }
            
        }
        if(GameObject.FindGameObjectWithTag("repairtrigger").GetComponent<RepairTrigger>().isBroken ==false){
            transform.position += new Vector3(finalSpeed, 0,0) * Time.deltaTime;
            finalSpeed = speedInit * acceleration;
        }
        else{
            finalSpeed = 0;
        }
        
    }

    void LateUpdate(){
        if(activemaps.Count >=3){
            Destroy(activemaps[0]);
            activemaps.RemoveAt(0);
        }
    }
}
