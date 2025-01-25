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
    public float maxRecoilX = 5f; 
    public float maxRecoilY = 2f; 
    public float recoilTime = 0.1f;
    public float returnTime = 0.2f;
    private bool isRecoiling = false;
    private Coroutine activeRecoilCoroutine = null;

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

        // Recoil etkisini ekle
        transform.rotation = Quaternion.Euler(xRotation - recoilX, yRotation + recoilY, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        if (Input.GetMouseButtonDown(0))
            ApplyRecoil();
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
            StopCoroutine(activeRecoilCoroutine); // Aktif recoil iþlemi varsa durdur
        }

        // Geçerli recoil deðerlerini al
        float initialRecoilX = recoilX;
        float initialRecoilY = recoilY;

        // Yeni recoil coroutine'ini baþlat
        activeRecoilCoroutine = StartCoroutine(RecoilCoroutine(initialRecoilX, initialRecoilY));
    }

    private IEnumerator RecoilCoroutine(float initialRecoilX, float initialRecoilY)
    {
        isRecoiling = true;

        // Geri tepme hareketi
        float elapsedTime = 0f;

        // Yeni hedef recoil deðerlerini belirle
        float targetRecoilX = Random.Range(maxRecoilX * 0.8f, maxRecoilX);
        float targetRecoilY = Random.Range(-maxRecoilY, maxRecoilY);

        // Yukarý doðru geri tepme
        while (elapsedTime < recoilTime)
        {
            elapsedTime += Time.deltaTime;
            recoilX = Mathf.Lerp(initialRecoilX, targetRecoilX, elapsedTime / recoilTime);
            recoilY = Mathf.Lerp(initialRecoilY, targetRecoilY, elapsedTime / recoilTime);
            yield return null;
        }

        // Baþlangýç pozisyonuna dönüþ
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
        isRecoiling = false;
    }
}
