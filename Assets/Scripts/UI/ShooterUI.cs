using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShooterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI maxAmmoText;

    public Image healthBar;

    private PlayerHealth shooterHealth;

    [SerializeField] private List<GameObject> ammoIcons;

    private int ammoCount = 25; 
    private int maxAmmo = 25;  
    void Start()
    {
        ammoText.text = ammoCount.ToString();
        maxAmmoText.text = maxAmmo.ToString();
        shooterHealth = GameObject.FindGameObjectWithTag("shooter").GetComponent<PlayerHealth>();
        UpdateAmmo(ammoCount);
    }

    public void UpdateAmmo(int count)
    {
        ammoCount = count;
        ammoText.text = ammoCount.ToString();

        for (int i = 0; i < ammoIcons.Count; i++)
        {
            bool isActive = i < ammoCount;
            ammoIcons[i].SetActive(isActive);
        }
    }

    public void UpdateHealth(float health)
    {
        health = shooterHealth.GetCurrentHealth();
        healthBar.fillAmount = health/10;
    }
}
