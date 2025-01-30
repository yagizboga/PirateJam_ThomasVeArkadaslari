using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteForHealth : MonoBehaviour
{
    public Volume postProcessVolume;
    private Vignette vignette;

    private PlayerMovement driverPlayerMovement;
    private PlayerMovement coalPlayerMovement;
    private PlayerMovement shooterPlayerMovement;

    private PlayerHealth driverPlayerHealth;
    private PlayerHealth coalPlayerHealth;
    private PlayerHealth shooterPlayerHealth;

    private void Start()
    {
        postProcessVolume.profile.TryGet(out vignette);

        driverPlayerMovement = GameObject.FindGameObjectWithTag("DriverPlayer").GetComponent<PlayerMovement>();
        coalPlayerMovement = GameObject.FindGameObjectWithTag("CoalPlayer").GetComponent<PlayerMovement>();
        shooterPlayerMovement = GameObject.FindGameObjectWithTag("shooter").GetComponent<PlayerMovement>();

        driverPlayerHealth = GameObject.FindGameObjectWithTag("DriverPlayer").GetComponent<PlayerHealth>();
        coalPlayerHealth = GameObject.FindGameObjectWithTag("CoalPlayer").GetComponent<PlayerHealth>();
        shooterPlayerHealth = GameObject.FindGameObjectWithTag("shooter").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        float healthPercentage = 1f;

        if (driverPlayerMovement.isActivePlayer)
        {
            healthPercentage = driverPlayerHealth.GetCurrentHealth()/ 10;
        }
        else if(coalPlayerMovement.isActivePlayer)
        {
            healthPercentage = coalPlayerHealth.GetCurrentHealth() / 10;
        }
        else if (shooterPlayerMovement.isActivePlayer)
        {
            healthPercentage = shooterPlayerHealth.GetCurrentHealth() / 10;
        }

        float intensityValue = Mathf.Lerp(0.2f, 0.5f, 1f - healthPercentage);
        vignette.intensity.Override(intensityValue); // Saðlýk azaldýkça vignette yoðunluðu artar

        float redValue = Mathf.Lerp(0, 120, 1f - healthPercentage) / 255f;
        vignette.color.Override(new Color(redValue, 0, 0)); // RGB Red deðeri 0'dan 120'ye çýkar
    }
}
