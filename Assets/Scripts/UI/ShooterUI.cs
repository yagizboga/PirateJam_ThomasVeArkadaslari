using UnityEngine;
using TMPro;

public class ShooterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI maxAmmoText;

    private int ammoCount = 25; 
    private int maxAmmo = 25;  
    void Start()
    {
        ammoText.text = ammoCount.ToString();
        maxAmmoText.text = maxAmmo.ToString();
    }

    public void UpdateAmmo(int count)
    {
        ammoText.text = count.ToString();
    }
}
