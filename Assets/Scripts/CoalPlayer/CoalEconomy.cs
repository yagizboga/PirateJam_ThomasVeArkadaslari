using UnityEngine;

public class CoalEconomy : MonoBehaviour
{
    private float maxCoal = 100f;
    private float coalBalance;
    public GameObject coal25;
    public GameObject coal50;
    public GameObject coal75;
    public GameObject coal100;
    private void Start()
    {
        coalBalance = maxCoal;
    }

    private void Update()
    {
        if(coalBalance > 0)
        {
            coalBalance -= Time.deltaTime;
            Debug.Log("Coal Balance: " + coalBalance);
        }
        else
        {
            coalBalance = 0f;
        }

        UpdateCoalVisual();

    }

    public void AddCoal(float amount)
    {
        if(coalBalance < maxCoal)
        {
            coalBalance += amount;
            if(coalBalance > maxCoal)
            {
                coalBalance = maxCoal;
            }
        }
        else
        {
            coalBalance = maxCoal;
        }
        
    }

    private void UpdateCoalVisual()
    {
        if (coalBalance < 0.1f)
        {
            coal25.SetActive(false);
            coal50.SetActive(false);
            coal75.SetActive(false);
            coal100.SetActive(false);
        }
        else if (coalBalance >= 0.1f && coalBalance < 25f)
        {
            coal25.SetActive(true);
            coal50.SetActive(false);
            coal75.SetActive(false);
            coal100.SetActive(false);
        }
        else if(coalBalance >= 25f && coalBalance < 50f)
        {
            coal25.SetActive(true);
            coal50.SetActive(true);
            coal75.SetActive(false);
            coal100.SetActive(false);
        }
        else if (coalBalance >= 50f && coalBalance < 75f)
        {
            coal25.SetActive(true);
            coal50.SetActive(true);
            coal75.SetActive(true);
            coal100.SetActive(false);
        }
        else if (coalBalance >= 75f)
        {
            coal25.SetActive(true);
            coal50.SetActive(true);
            coal75.SetActive(true);
            coal100.SetActive(true);
        }

    }
}
