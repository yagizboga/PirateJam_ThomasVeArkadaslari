using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 3;
    public GameObject ragdoll;

    public void TakeDamage(int hit)
    {
        health -= hit;
        if (health <= 0)
        {
            SpawnRagdoll();
            Destroy(gameObject);
            health += 10;
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
            Destroy(spawnedRagdoll, 20f);
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
