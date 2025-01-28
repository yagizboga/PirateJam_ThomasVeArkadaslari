using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private GameObject driverPlayer;    // 1st
    private GameObject coalPlayer;      // 2nd
    private GameObject shooterPlayer;   // 3rd

    private PlayerMovement driverPlayerMovement;
    private PlayerMovement coalPlayerMovement;
    private PlayerMovement shooterPlayerMovement;

    private void Start()
    {
        driverPlayer = GameObject.FindGameObjectWithTag("DriverPlayer");
        coalPlayer = GameObject.FindGameObjectWithTag("CoalPlayer");
        shooterPlayer = GameObject.FindGameObjectWithTag("shooter");

        driverPlayerMovement = driverPlayer.GetComponent<PlayerMovement>();
        coalPlayerMovement = coalPlayer.GetComponent<PlayerMovement>();
        shooterPlayerMovement = shooterPlayer.GetComponent<PlayerMovement>();
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
