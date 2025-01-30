using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShooterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI maxAmmoText;

    public Image healthBar;
    private int healthAmount = 10;

    private PlayerHealth shooterHealth;
    

    private int ammoCount = 25; 
    private int maxAmmo = 25;  
    void Start()
    {
        ammoText.text = ammoCount.ToString();
        maxAmmoText.text = maxAmmo.ToString();
        shooterHealth = GameObject.FindGameObjectWithTag("shooter").GetComponent<PlayerHealth>();
    }

    public void UpdateAmmo(int count)
    {
        ammoText.text = count.ToString();
    }

    public void UpdateHealth(float health)
    {
        health = shooterHealth.GetCurrentHealth();
        healthBar.fillAmount = health/10;
    }
}
