using UnityEngine;

public class RepairTrigger : MonoBehaviour
{
    private GameObject driver;
    private Repair repair;

    private bool canRepair = false; 
    private float repairTime = 10f; 
    private float repairTimer = 0f;

    private bool isBroken = true; // false yap bunu, test icin true durumda

    private void Start()
    {
        driver = GameObject.FindGameObjectWithTag("DriverPlayer");
        repair = driver.GetComponent<Repair>();
    }
    private void Update()
    {
        if (canRepair && Input.GetMouseButton(0) && isBroken)
        {
            repairTimer += Time.deltaTime;
            Debug.Log(repairTimer);
            if (repairTimer >= repairTime)
            {
                Debug.Log("Repair completed");
                isBroken = false;
                canRepair = false;
                repair.SetCanRepair(false);
                repairTimer = 0f;

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            repairTimer = 0f;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DriverPlayer") && isBroken)
        {
            canRepair = true;
            repair.SetCanRepair(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DriverPlayer"))
        {
            canRepair = false;
            repair.SetCanRepair(false);
            repairTimer = 0f;
        }
    }

    public void SetIsBroken(bool broken)
    {
        isBroken = broken;
    }
}
