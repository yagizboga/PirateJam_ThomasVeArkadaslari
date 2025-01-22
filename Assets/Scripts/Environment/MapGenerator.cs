using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GameObject terrain0;
    [SerializeField] GameObject terrain1;
    List<GameObject> maps;
    Grid grid;
    void Start(){
        maps = new List<GameObject>();
        grid = new Grid(3,1,200);
        maps.Add(Instantiate(terrain0,grid.GetMap(0,0),Quaternion.identity,this.transform));
        maps.Add(Instantiate(terrain1,grid.GetMap(1,0),Quaternion.identity,this.transform));
    }

    public void GenerateNextMap(){
        maps.Add(Instantiate(terrain1,grid.GetMap(1,0),Quaternion.identity,this.transform));
        Debug.Log(grid.GetMap(1,0));
    }

    void Update(){
        if(Input.GetKey("w")){
            transform.position += new Vector3(-300,0,0) * Time.deltaTime;
        }
    }

    void LateUpdate(){
        if(maps.Count >=4){
            Destroy(maps[0]);
            maps.RemoveAt(0);
        }
    }
}
