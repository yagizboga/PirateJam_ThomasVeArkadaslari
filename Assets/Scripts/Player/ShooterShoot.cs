using Unity.VisualScripting;
using UnityEngine;

public class ShooterShoot : MonoBehaviour
{
    [SerializeField] Camera maincamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            //burada ateş animasyonunun triggerını başlat ve animasyona shoot eventi ata şimdilik fonksiyonu burada çalıştırdım
            Shoot();
        }
    }

    void Shoot(){
        RaycastHit hit;
        if(Physics.Raycast(maincamera.transform.position,maincamera.transform.forward,out hit)){
            if(hit.collider.CompareTag("enemy")){
                Debug.Log("enemy hit!");
            }
        }
    }
}
