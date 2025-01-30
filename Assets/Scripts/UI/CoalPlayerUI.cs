using UnityEngine;
using UnityEngine.UI;

public class CoalPlayerUI : MonoBehaviour
{
    public Image fuelBar;
    private float fuelAmount = 100f;
    private CoalEconomy coalEconomy;

    public Image healthBar;
    private int healthAmount = 10;
    private PlayerHealth coalPlayerHealth;
    private void Start()
    {
        coalEconomy = GameObject.FindGameObjectWithTag("Oven").GetComponent<CoalEconomy>();
        coalPlayerHealth = GameObject.FindGameObjectWithTag("CoalPlayer").GetComponent<PlayerHealth>();

    }

    private void Update()
    {
        fuelAmount = coalEconomy.GetCoalBalance();
        fuelBar.fillAmount = fuelAmount / 100f;
    }

    public void UpdateHealth(float health)
    {
        health = coalPlayerHealth.GetCurrentHealth();
        healthBar.fillAmount = health / 10;
    }
}
