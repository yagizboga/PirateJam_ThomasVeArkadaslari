using System.Collections;
using UnityEngine;

public class ZeplinMovement : MonoBehaviour
{
    public Transform deployPosition;
    public Transform fleePosition;
    public Transform spawnRoof;
    public Transform spawnFloor;
    public GameObject enemy;

    private bool isMoving = false;

    private float spawnCoolDown = 3f;

    void Start()
    {
        StartCoroutine(MoveToDeploy());
    }

    private IEnumerator MoveToDeploy()
    {
        isMoving = true;
        Vector3 targetPosition = new Vector3(deployPosition.position.x, transform.position.y, deployPosition.position.z);

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;

        StartCoroutine(EnemySpawn());
    }

    private IEnumerator EnemySpawn()
    {
        float spawnDuration = Random.Range(10f, 15f); // chooses a random spawn duration
        float elapsedTime = 0f;

        while (elapsedTime < spawnDuration)
        {
            Transform spawnPoint = Random.Range(0, 2) == 0 ? spawnRoof : spawnFloor;
            Instantiate(enemy, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnCoolDown); // 3s
            elapsedTime += spawnCoolDown;
        }

        StartCoroutine(MoveToFlee());
    }

    private IEnumerator MoveToFlee()
    {
        isMoving = true;
        Vector3 targetPosition = new Vector3(fleePosition.position.x, transform.position.y, fleePosition.position.z);

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }
}
