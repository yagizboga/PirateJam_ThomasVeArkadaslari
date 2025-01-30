using UnityEngine;
using UnityEngine.UI;

public class CoalPlayerUI : MonoBehaviour
{
    public Image fuelBar;
    private float fuelAmount = 100f;
    private CoalEconomy coalEconomy;

    private void Start()
    {
        coalEconomy = GameObject.FindGameObjectWithTag("Oven").GetComponent<CoalEconomy>();

    }

    private void Update()
    {
        fuelAmount = coalEconomy.GetCoalBalance();
        fuelBar.fillAmount = fuelAmount / 100f;
    }
}
