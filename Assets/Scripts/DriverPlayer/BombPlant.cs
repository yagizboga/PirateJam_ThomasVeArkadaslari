using System.Collections;
using UnityEngine;

public class BombPlant : MonoBehaviour
{
    private bool canPlant = false;
    public GameObject bombObj;
    public GameObject bombHighlightObj;
    public ParticleSystem explodeParticle;
    [SerializeField] GameObject deletedobject;

    private void Start()
    {
        bombObj.SetActive(false);
        bombHighlightObj.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPlant)
        {
            canPlant = false;
            PlantBomb();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DriverPlayer"))
        {
            canPlant = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DriverPlayer"))
        {
            canPlant = false;
        }
    }

    private void PlantBomb()
    {
        bombObj.SetActive(true);
        bombHighlightObj.SetActive(false);
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(5f);
        explodeParticle.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(deletedobject);
    }
}
