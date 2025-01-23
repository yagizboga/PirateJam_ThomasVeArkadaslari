using UnityEngine;

public class CoalBox : MonoBehaviour
{
    public CoalPlayer coal_player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shovel"))
        {
            coal_player.SetInCoalBox(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shovel"))
        {
            coal_player.SetInCoalBox(false);
        }
    }
}
