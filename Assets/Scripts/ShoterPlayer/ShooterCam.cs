using System.Collections;
using UnityEngine;

public class ShooterCam : MonoBehaviour
{
    private float sensX = 400f;
    private float sensY = 400f;

    public Transform orientation;
    public Transform player;

    private float xRotation;
    private float yRotation;

    public GameObject bodySpine;

    private float recoilX = 0f;
    private float recoilY = 0f;
    public float recoilSmoothness = 5f; // not effecting anymore
    public float maxRecoilX = 6f; 
    public float maxRecoilY = 1.5f; 
    public float recoilTime = 0.1f;
    public float returnTime = 0.2f;
    //private bool isRecoiling = false;
    private Coroutine activeRecoilCoroutine = null;

    public GameObject rifle;
    private float rifleRecoilZ = 0f;
    private float rifleRecoilY = 0f;
    public float maxRifleRecoilZ = 0.1f;
    public float maxRifleRecoilY = 0.01f;
    private Coroutine activeRifleRecoilCoroutine = null;
    private Vector3 initialRiflePosition;



    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        initialRiflePosition = rifle.transform.localPosition;
    }

    private void FixedUpdate()
    {
        player.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation - recoilX, yRotation + recoilY, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }

    private void LateUpdate()
    {
        float targetXRotation = Mathf.Clamp(xRotation / 3, -30f, 30f);
        bodySpine.transform.localRotation = Quaternion.Euler(targetXRotation, 0, 0);
    }

    public void ApplyRecoil()
    {
        if (activeRecoilCoroutine != null)
        {
            StopCoroutine(activeRecoilCoroutine); 
        }

        float initialRecoilX = recoilX;
        float initialRecoilY = recoilY;

        activeRecoilCoroutine = StartCoroutine(RecoilCoroutine(initialRecoilX, initialRecoilY));
        ApplyRecoilForRifle();
    }

    private IEnumerator RecoilCoroutine(float initialRecoilX, float initialRecoilY)
    {
        float elapsedTime = 0f;

        float targetRecoilX = Random.Range(maxRecoilX * 0.8f, maxRecoilX);
        float targetRecoilY = Random.Range(-maxRecoilY, maxRecoilY);

        while (elapsedTime < recoilTime)
        {
            elapsedTime += Time.deltaTime;
            recoilX = Mathf.Lerp(initialRecoilX, targetRecoilX, elapsedTime / recoilTime);
            recoilY = Mathf.Lerp(initialRecoilY, targetRecoilY, elapsedTime / recoilTime);
            yield return null;
        }

        elapsedTime = 0f;
        initialRecoilX = recoilX;
        initialRecoilY = recoilY;

        while (elapsedTime < returnTime)
        {
            elapsedTime += Time.deltaTime;
            recoilX = Mathf.Lerp(initialRecoilX, 0, elapsedTime / returnTime);
            recoilY = Mathf.Lerp(initialRecoilY, 0, elapsedTime / returnTime);
            yield return null;
        }

        recoilX = 0f;
        recoilY = 0f;
    }

    public void ApplyRecoilForRifle()
    {
        if (activeRifleRecoilCoroutine != null)
        {
            StopCoroutine(activeRifleRecoilCoroutine); 
        }

        float initialRifleRecoilZ = rifleRecoilZ;
        float initialRifleRecoilY = rifleRecoilY;

        activeRifleRecoilCoroutine = StartCoroutine(RifleRecoilCoroutine(initialRifleRecoilZ, initialRifleRecoilY));
    }

    private IEnumerator RifleRecoilCoroutine(float initialRecoilZ, float initialRecoilY)
    {
        float elapsedTime = 0f;

        /*float targetRecoilZ = Random.Range(-maxRifleRecoilZ, 0f); 
        float targetRecoilY = Random.Range(0f, maxRifleRecoilY); */

        float targetRecoilZ = -maxRifleRecoilZ; // randomizing these values doesn't look like good,
        float targetRecoilY = maxRifleRecoilY;  // thus, it is decided to keep them spesific values

        while (elapsedTime < recoilTime)
        {
            elapsedTime += Time.deltaTime;
            rifle.transform.localPosition = Vector3.Lerp(initialRiflePosition, new Vector3(initialRiflePosition.x, initialRiflePosition.y + targetRecoilY, initialRiflePosition.z + targetRecoilZ), elapsedTime / recoilTime);
            yield return null;
        }

        elapsedTime = 0f;
        Vector3 initialRiflePositionReturn = rifle.transform.localPosition;

        while (elapsedTime < returnTime)
        {
            elapsedTime += Time.deltaTime;
            rifle.transform.localPosition = Vector3.Lerp(initialRiflePositionReturn, initialRiflePosition, elapsedTime / returnTime);
            yield return null;
        }

        rifle.transform.localPosition = initialRiflePosition;
    }
}
