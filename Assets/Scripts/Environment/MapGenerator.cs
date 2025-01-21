using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GameObject testmap;
    void Start(){
        Grid grid = new Grid(3,3,100);
        Instantiate(testmap,grid.GetMap(0,0),Quaternion.identity);
        Instantiate(testmap,grid.GetMap(2,0),Quaternion.identity);
        Instantiate(testmap,grid.GetMap(1,0),Quaternion.identity);
        
    }




}
