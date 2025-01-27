using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2f); 
    }
}
