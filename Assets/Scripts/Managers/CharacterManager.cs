using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private GameObject driverPlayerObj;    // 1st
    private GameObject coalPlayerObj;      // 2nd
    private GameObject shooterPlayerObj;   // 3rd

    private PlayerMovement driverPlayerMovement;
    private PlayerMovement coalPlayerMovement;
    private PlayerMovement shooterPlayerMovement;

    private CoalPlayer coalPlayerScript;

    private void Start()
    {
        driverPlayerObj = GameObject.FindGameObjectWithTag("DriverPlayer");
        coalPlayerObj = GameObject.FindGameObjectWithTag("CoalPlayer");
        shooterPlayerObj = GameObject.FindGameObjectWithTag("shooter");

        driverPlayerMovement = driverPlayerObj.GetComponent<PlayerMovement>();
        coalPlayerMovement = coalPlayerObj.GetComponent<PlayerMovement>();
        shooterPlayerMovement = shooterPlayerObj.GetComponent<PlayerMovement>();

        coalPlayerScript = coalPlayerObj.GetComponent<CoalPlayer>();
    }

    private void Update()
    {
        ChangePlayer();
    }

    private void ChangePlayer()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(driverPlayerMovement.isActivePlayer == false)
            {
                driverPlayerMovement.SetIsActivePlayer(true);
                coalPlayerMovement.SetIsActivePlayer(false);
                shooterPlayerMovement.SetIsActivePlayer(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (coalPlayerMovement.isActivePlayer == false)
            {
                driverPlayerMovement.SetIsActivePlayer(false);
                coalPlayerMovement.SetIsActivePlayer(true);
                shooterPlayerMovement.SetIsActivePlayer(false);

                coalPlayerScript.SetActiveShovel();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (shooterPlayerMovement.isActivePlayer == false)
            {
                driverPlayerMovement.SetIsActivePlayer(false);
                coalPlayerMovement.SetIsActivePlayer(false);
                shooterPlayerMovement.SetIsActivePlayer(true);
            }
        }
    }
}
