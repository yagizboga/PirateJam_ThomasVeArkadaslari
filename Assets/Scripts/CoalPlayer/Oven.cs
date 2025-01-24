using UnityEngine;

public class Oven : MonoBehaviour
{
    private CoalPlayer coal_player;

    private void Start()
    {
        coal_player = GameObject.FindGameObjectWithTag("CoalPlayer").GetComponent<CoalPlayer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shovel"))
        {
            coal_player.SetInOven(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shovel"))
        {
            coal_player.SetInOven(false);
        }
    }
}
