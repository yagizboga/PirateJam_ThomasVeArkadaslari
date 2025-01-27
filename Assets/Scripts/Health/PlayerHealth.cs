using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    private int health;
    public GameObject ragdoll;

    private void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(int hit)
    {
        health -= hit;
        //Debug.Log(health);
        if (health <= 0)
        {
            SpawnRagdoll();
            Destroy(gameObject);
        }
    }
    private void SpawnRagdoll()
    {
        if (ragdoll != null)
        {
            GameObject spawnedRagdoll = Instantiate(ragdoll, transform.position, transform.rotation);

            Animator ragdollAnimator = spawnedRagdoll.GetComponent<Animator>();
            if (ragdollAnimator != null)
            {
                ragdollAnimator.enabled = false;
            }

            CopyTransforms(transform, spawnedRagdoll.transform);
            //Destroy(spawnedRagdoll, 20f);
        }
    }

    private void CopyTransforms(Transform source, Transform destination)
    {
        for (int i = 0; i < source.childCount; i++)
        {
            Transform sourceChild = source.GetChild(i);
            Transform destinationChild = destination.Find(sourceChild.name);

            if (destinationChild != null)
            {
                destinationChild.position = sourceChild.position;
                destinationChild.rotation = sourceChild.rotation;

                CopyTransforms(sourceChild, destinationChild);
            }
        }
    }

}
