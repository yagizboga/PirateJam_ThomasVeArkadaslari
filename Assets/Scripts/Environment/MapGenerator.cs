using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GameObject terrain0;
    [SerializeField] GameObject terrain1;
    List<GameObject> maps;
    Grid grid;

    private float speedInit = 10f;
    private float acceleration = 10f;
    private float finalSpeed;
    void Start(){
        maps = new List<GameObject>();
        grid = new Grid(5,1,200);
        maps.Add(Instantiate(terrain0,grid.GetMap(0,0),Quaternion.identity,this.transform));
        maps.Add(Instantiate(terrain1,grid.GetMap(1,0),Quaternion.identity,this.transform));
        maps.Add(Instantiate(terrain1, grid.GetMap(2, 0), Quaternion.identity, this.transform));
        maps.Add(Instantiate(terrain1, grid.GetMap(3, 0), Quaternion.identity, this.transform));
        maps.Add(Instantiate(terrain1, grid.GetMap(4, 0), Quaternion.identity, this.transform));
    }

    public void GenerateNextMap(){
        maps.Add(Instantiate(terrain1,grid.GetMap(4,0),Quaternion.identity,this.transform));
        Debug.Log(grid.GetMap(4,0));
    }

    void FixedUpdate(){
        if(Input.GetKey("w")){
            finalSpeed = speedInit * acceleration;
            transform.position += new Vector3(-finalSpeed, 0,0) * Time.deltaTime;
        }
    }

    void LateUpdate(){
        if(maps.Count >=15){
            Destroy(maps[0]);
            maps.RemoveAt(0);
        }
    }
}
