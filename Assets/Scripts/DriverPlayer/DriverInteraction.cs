using UnityEngine;

public class DriverInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;   
    public float sphereRadius = 0.5f;       
    public Camera playerCamera;            

    private GameObject lastHighlightedObject; 
    private Material originalMaterial;       
    public Material highlightMaterial;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (playerMovement.isActivePlayer)
        {
            RaycastHit hit;

            if (Physics.SphereCast(playerCamera.transform.position, sphereRadius, playerCamera.transform.forward, out hit, interactionDistance))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.CompareTag("Gas") || hitObject.CompareTag("Brake"))
                {
                    HighlightObject(hitObject);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (hitObject.CompareTag("Gas"))
                        {
                            Debug.Log("Gas interacted!");
                        }
                        else if (hitObject.CompareTag("Brake"))
                        {
                            Debug.Log("Brake interacted!");
                        }
                    }
                }
                else
                {
                    ClearHighlight();
                }
            }
            else
            {
                ClearHighlight();
            }
        }
    }

    private void HighlightObject(GameObject obj)
    {
        if (lastHighlightedObject == obj) return; 

        ClearHighlight(); 

        Renderer objRenderer = obj.GetComponent<Renderer>();
        if (objRenderer != null)
        {
            originalMaterial = objRenderer.material;
            objRenderer.material = highlightMaterial; 
            lastHighlightedObject = obj;
        }
    }

    private void ClearHighlight()
    {
        if (lastHighlightedObject != null)
        {
            Renderer objRenderer = lastHighlightedObject.GetComponent<Renderer>();
            if (objRenderer != null && originalMaterial != null)
            {
                objRenderer.material = originalMaterial;
            }
            lastHighlightedObject = null;
            originalMaterial = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactionDistance);
            Gizmos.DrawWireSphere(playerCamera.transform.position + playerCamera.transform.forward * interactionDistance, sphereRadius);
        }
    }
}
