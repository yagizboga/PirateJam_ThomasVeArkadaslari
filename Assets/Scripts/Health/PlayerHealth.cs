using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 10;
    private int health;
    public GameObject ragdollActive;
    public GameObject ragdollDeactive;

    private int regenAmount = 1; 
    private float regenDelay = 10f; 
    private float lastDamageTime;

    private bool isDead = false;

    private PlayerMovement playerMovement;

    [SerializeField] private ShooterUI shooterUI;
    [SerializeField] private DriverUI driverUI;
    [SerializeField] private CoalPlayerUI coalPlayerUI;

    private bool isRegening = false;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        health = maxHealth;
        lastDamageTime = Time.time;
        StartCoroutine(HealthRegen());
    }

    public void TakeDamage(int hit)
    {
        health -= hit;
        lastDamageTime = Time.time;

        UpdateHealthUI();


        if (health <= 0 && !isDead)
        {
            isDead = true;
            bool isActivePlayer = playerMovement.isActivePlayer;
            SpawnRagdoll(isActivePlayer);
            Destroy(gameObject);
        }
    }
    private void SpawnRagdoll(bool isActivePlayer)
    {
        if (ragdollActive != null && isActivePlayer)
        {
            GameObject spawnedRagdoll = Instantiate(ragdollActive, transform.position, transform.rotation);

            Animator ragdollAnimator = spawnedRagdoll.GetComponent<Animator>();
            if (ragdollAnimator != null)
            {
                ragdollAnimator.enabled = false;
            }

            CopyTransforms(transform, spawnedRagdoll.transform);
        }
        else if (ragdollDeactive != null && !isActivePlayer)
        {
            GameObject spawnedRagdoll = Instantiate(ragdollDeactive, transform.position, transform.rotation);

            Animator ragdollAnimator = spawnedRagdoll.GetComponent<Animator>();
            if (ragdollAnimator != null)
            {
                ragdollAnimator.enabled = false;
            }

            CopyTransforms(transform, spawnedRagdoll.transform);
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

    private IEnumerator HealthRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1); 

            if (Time.time - lastDamageTime >= regenDelay && health < maxHealth)
            {
                health += regenAmount;
                UpdateHealthUI();
                isRegening = true;
                health = Mathf.Clamp(health, 0, maxHealth);
                //Debug.Log($"Health regenerated to: {health}");
            }
            if(health == maxHealth)
            {
                isRegening = false;
            }
        }
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public int GetCurrentHealth()
    {
        return health;
    }

    public bool GetIsRegening() { return isRegening; }

    private void UpdateHealthUI()
    {
        if (gameObject.CompareTag("shooter"))
        {
            shooterUI.UpdateHealth(health);
            //Debug.Log("shooter health: " + health);
        }


        if (gameObject.CompareTag("DriverPlayer"))
        {
            driverUI.UpdateHealth(health);
            //Debug.Log("DriverPlayer health: " + health);
        }


        if (gameObject.CompareTag("CoalPlayer"))
        {
            coalPlayerUI.UpdateHealth(health);
            //Debug.Log("CoalPlayer health: " + health);
        }
    }

}
