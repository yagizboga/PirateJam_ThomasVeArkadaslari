using UnityEngine;
using UnityEngine.UI;

public class DriverUI : MonoBehaviour
{
    public Image trainHealthBar;

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

    public void SetStopSign(bool set)
    {
        stopSignActive.SetActive(set);
        stopSignDeactive.SetActive(!set);
    }

    public void SetRepairSign(bool set)
    {
        repairSignActive.SetActive(set);
        repairSignDeactive.SetActive(!set);
    }
    
        

}
