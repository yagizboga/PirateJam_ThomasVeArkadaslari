using UnityEngine;

public class DriverInteraction : MonoBehaviour
{

    public float interactionDistance = 3f;
    public Camera playerCamera;

    void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("Gas"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Gas interacted!");
                }
            }
            else if (hit.collider.CompareTag("Brake"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Brake interacted!");
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactionDistance);
        }
    }
}
