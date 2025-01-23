using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    private float sensX = 400f;
    private float sensY = 400f;

    public Transform orientation;
    public Transform player;

    private float xRotation;
    private float yRotation;

    public GameObject bodySpine;

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

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        /*float spineRotationX = Mathf.Clamp(xRotation / 3f, -30f, 30f); // bu kýsýmlar animayon yuzuden calismamaya basladi, late update ile cozdum ama
        bodySpine.transform.localRotation = Quaternion.Euler(spineRotationX, 0, 0);/////////*/

    }

    private void LateUpdate()
    {
        // late update, animasyonlardan da sonra calisiyormus, bu sayede animasyon hareketi uzerinde ek hareketler saglayabiliyoruz kod ile
        float targetXRotation = Mathf.Clamp(xRotation / 3, -30f, 30f); 
        bodySpine.transform.localRotation = Quaternion.Euler(targetXRotation, 0, 0);
    }
}
