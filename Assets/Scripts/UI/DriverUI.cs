using UnityEngine;
using UnityEngine.UI;

public class DriverUI : MonoBehaviour
{
    public Image trainHealthBar;
    private float healthAmount = 100f;


    public void UpdateTrainHealthUI(float health)
    {
        trainHealthBar.fillAmount = health / 100f;
    }
    
        

}
