using UnityEngine;

public class TrainHealth : MonoBehaviour
{
    private float trainMaxHealth = 100f;
    private float trainCurrentHealth;

    private DriverUI driverUI;

    private void Start()
    {
        driverUI = GameObject.FindGameObjectWithTag("DriverUI").GetComponent<DriverUI>();
        trainCurrentHealth = trainMaxHealth;
    }

    public void TrainTakeDamage(float damage)
    {
        if(trainCurrentHealth > 0f)
        {
            trainCurrentHealth -= damage;
            driverUI.UpdateTrainHealthUI(trainCurrentHealth);
        }
        else
        {
            //death screen
        }
        
    }


    public float GetTrainHealth() 
    {
        return trainCurrentHealth; 
    }
}
