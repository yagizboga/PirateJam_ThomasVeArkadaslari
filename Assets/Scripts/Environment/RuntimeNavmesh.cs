using UnityEngine;
using Unity.AI.Navigation;


[RequireComponent(typeof(NavMeshSurface))]
public class RuntimeNavmesh : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;
    GameObject[] characters;
    Transform[] spawnpoints;

    void Awake(){
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    void Update(){
        navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
    }
}
