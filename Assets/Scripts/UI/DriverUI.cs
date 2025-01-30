using UnityEngine;
using UnityEngine.UI;

public class DriverUI : MonoBehaviour
{
    public Image trainHealthBar;
    private float healthAmount = 100f;

    public GameObject stopSignActive;
    public GameObject stopSignDeactive;

    public GameObject repairSignActive;
    public GameObject repairSignDeactive;

    private void Start()
    {
        stopSignActive.SetActive(false);
        repairSignActive.SetActive(false);

        stopSignDeactive.SetActive(true);
        repairSignDeactive.SetActive(true);
    }


    public void UpdateTrainHealthUI(float health)
    {
        trainHealthBar.fillAmount = health / 100f;
    }
    
        

}
