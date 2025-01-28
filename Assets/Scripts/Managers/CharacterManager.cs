using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour
{
    private GameObject driverPlayerObj;    // 1st
    private GameObject coalPlayerObj;      // 2nd
    private GameObject shooterPlayerObj;   // 3rd

    private GameObject activePlayer;

    private PlayerMovement driverPlayerMovement;
    private PlayerMovement coalPlayerMovement;
    private PlayerMovement shooterPlayerMovement;

    private PlayerHealth driverPlayerHealth;
    private PlayerHealth coalPlayerHealth;
    private PlayerHealth shooterPlayerHealth;

    private CoalPlayer coalPlayerScript;

    [SerializeField] private GameObject transitionCAM; 
    [SerializeField] private float cameraMoveSpeed = 5f; // 5f for jam, less for later
    [SerializeField] private float cameraHeight = 25f;
    [SerializeField] private float cameraOffsetFromHeadUp = 1.5f;
    private bool isInTransition = false;

    [SerializeField] private float XZsmallOffset = 0.5f;
    [SerializeField] private float YsmallOffset = 0.6f;

    private bool isDead = false;
    

    private void Start()
    {
        driverPlayerObj = GameObject.FindGameObjectWithTag("DriverPlayer");
        coalPlayerObj = GameObject.FindGameObjectWithTag("CoalPlayer");
        shooterPlayerObj = GameObject.FindGameObjectWithTag("shooter");

        driverPlayerMovement = driverPlayerObj.GetComponent<PlayerMovement>();
        coalPlayerMovement = coalPlayerObj.GetComponent<PlayerMovement>();
        shooterPlayerMovement = shooterPlayerObj.GetComponent<PlayerMovement>();

        driverPlayerHealth = driverPlayerObj.GetComponent<PlayerHealth>();
        coalPlayerHealth = coalPlayerObj.GetComponent<PlayerHealth>();
        shooterPlayerHealth = shooterPlayerObj.GetComponent<PlayerHealth>();

        coalPlayerScript = coalPlayerObj.GetComponent<CoalPlayer>();

        transitionCAM.SetActive(false); 
        
        if(driverPlayerMovement.isActivePlayer) activePlayer = driverPlayerObj; 
        else if(coalPlayerMovement.isActivePlayer) activePlayer = coalPlayerObj;
        else if (shooterPlayerMovement.isActivePlayer) activePlayer = shooterPlayerObj;
    }

    private void Update()
    {
        IsDeadCheck();
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && activePlayer != driverPlayerObj && !isInTransition)
            {
                StartCoroutine(ChangePlayer(driverPlayerObj, driverPlayerMovement));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && activePlayer != coalPlayerObj && !isInTransition)
            {
                StartCoroutine(ChangePlayer(coalPlayerObj, coalPlayerMovement));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && activePlayer != shooterPlayerObj && !isInTransition)
            {
                StartCoroutine(ChangePlayer(shooterPlayerObj, shooterPlayerMovement));
            }
        }
    }

    private void LateUpdate()
    {
        FollowActivePlayerXZ();
    }

    private IEnumerator ChangePlayer(GameObject nextPlayer, PlayerMovement nextPlayerMovement)
    {
        isInTransition = true;
        if (activePlayer != null)
        {
            var currentPlayerMovement = activePlayer.GetComponent<PlayerMovement>();
            currentPlayerMovement.SetIsActivePlayer(false);
        }
        Time.timeScale = 0.5f;

        transitionCAM.SetActive(true);
        Vector3 initialPosition = transitionCAM.transform.position;
        Vector3 targetPosition = new Vector3(initialPosition.x, cameraHeight, initialPosition.z);
    
        yield return MoveToPositionY(transitionCAM, targetPosition.y);

        targetPosition = new Vector3(nextPlayer.transform.position.x, cameraHeight, nextPlayer.transform.position.z);
        yield return MoveToPositionXZ(transitionCAM, targetPosition);

        targetPosition = new Vector3(nextPlayer.transform.position.x, nextPlayer.transform.position.y + cameraOffsetFromHeadUp, nextPlayer.transform.position.z);
        yield return MoveToPositionY(transitionCAM, targetPosition.y);

        transitionCAM.SetActive(false);
        nextPlayerMovement.SetIsActivePlayer(true);
        activePlayer = nextPlayer;
        isInTransition = false;

        if (activePlayer == coalPlayerObj)
        {
            coalPlayerScript.SetActiveShovel();
        }
        if(activePlayer == shooterPlayerObj)
        {
            shooterPlayerMovement.SetAnimatorActivePlayer(true);
        }
        Time.timeScale = 1f;
    }

    private IEnumerator MoveToPositionY(GameObject obj, float targetY)
    {
        Vector3 initialPosition = obj.transform.position;
        Vector3 targetPosition = new Vector3(initialPosition.x, targetY, initialPosition.z);

        while (Mathf.Abs(obj.transform.position.y - targetY) > YsmallOffset)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, targetPosition, cameraMoveSpeed * Time.unscaledDeltaTime);
            yield return null;
        }

        obj.transform.position = targetPosition;
    }

    private IEnumerator MoveToPositionXZ(GameObject obj, Vector3 targetPosition)
    {
        while (Vector3.Distance(new Vector3(obj.transform.position.x, 0, obj.transform.position.z), new Vector3(targetPosition.x, 0, targetPosition.z)) > XZsmallOffset)
        {
            Vector3 newPosition = Vector3.Lerp(obj.transform.position, targetPosition, cameraMoveSpeed * Time.unscaledDeltaTime);
            obj.transform.position = new Vector3(newPosition.x, obj.transform.position.y, newPosition.z);
            yield return null;
        }

        obj.transform.position = new Vector3(targetPosition.x, obj.transform.position.y, targetPosition.z);
    }

    private void FollowActivePlayerXZ()
    {
        if (transitionCAM.activeSelf) return; 

        if (activePlayer != null)
        {
            Vector3 currentPosition = transitionCAM.transform.position;
            Vector3 targetPosition = new Vector3(activePlayer.transform.position.x, activePlayer.transform.position.y + cameraOffsetFromHeadUp, activePlayer.transform.position.z);
            transitionCAM.transform.position = targetPosition;
        }
    }

    private void IsDeadCheck()
    {
        if (driverPlayerHealth.GetIsDead() || coalPlayerHealth.GetIsDead() || shooterPlayerHealth.GetIsDead())
        {
            isDead = true;
        }
    }
}
