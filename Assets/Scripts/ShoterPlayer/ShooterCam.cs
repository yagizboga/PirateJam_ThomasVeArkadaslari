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

    // Recoil deðiþkenleri
    private float recoilX = 0f;
    private float recoilY = 0f;
    public float recoilSmoothness = 5f; // Geri dönüþ pürüzsüzlüðü
    public float maxRecoilX = 5f; // Y ekseninde maksimum geri tepme
    public float maxRecoilY = 2f; // X ekseninde maksimum geri tepme
    private bool isRecoiling = false; // Recoil iþlemi aktif mi

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
        if (isRecoiling) return; // Zaten bir recoil iþlemi devam ediyorsa yenisini baþlatma
        StartCoroutine(RecoilCoroutine());
    }

    private IEnumerator RecoilCoroutine()
    {
        isRecoiling = true;

        // Geri tepme hareketi
        float recoilTime = 0.1f; // Geri tepme süresi
        float returnTime = 0.2f; // Geri dönüþ süresi
        float elapsedTime = 0f;

        float initialRecoilX = 0f;
        float targetRecoilX = Random.Range(maxRecoilX * 0.8f, maxRecoilX);

        float initialRecoilY = 0f;
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
