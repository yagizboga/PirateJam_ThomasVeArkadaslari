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
    public float recoilSmoothness = 20f; 
    public float maxRecoilX = 1f; // Y recoil
    public float maxRecoilY = 0.5f; // X recoil

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

        xRotation -= recoilX;
        yRotation += recoilY;

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        recoilX = Mathf.Lerp(recoilX, 0, Time.deltaTime * recoilSmoothness);
        recoilY = Mathf.Lerp(recoilY, 0, Time.deltaTime * recoilSmoothness);

        if (Input.GetMouseButtonDown(0))
        {
            ApplyRecoil();
        }
    }

    private void LateUpdate()
    {
        float targetXRotation = Mathf.Clamp(xRotation / 3, -30f, 30f);
        bodySpine.transform.localRotation = Quaternion.Euler(targetXRotation, 0, 0);
    }

    public void ApplyRecoil()
    {
        recoilX += Random.Range(maxRecoilX * 0.8f, maxRecoilX); 
        recoilY += Random.Range(-maxRecoilY, maxRecoilY); 
    }
}
