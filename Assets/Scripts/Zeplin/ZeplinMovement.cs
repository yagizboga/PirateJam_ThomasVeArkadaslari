using System.Collections;
using UnityEngine;

public class ZeplinMovement : MonoBehaviour
{
    public Transform deployPosition;
    public Transform fleePosition;
    public Transform spawnRoof;
    public Transform spawnFloor;
    public GameObject enemy;

    private float spawnCoolDown = 4f;
    private float moveDuration = 4f; 

    void Start()
    {
        CallZeplin(); // for test
    }

    public void CallZeplin()
    {
        StartCoroutine(MoveToDeploy());
    }

    IEnumerator MoveToDeploy()
    {
        yield return StartCoroutine(MoveToPosition(deployPosition.position, moveDuration));

        yield return new WaitForSeconds(2f);
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        float spawnDuration = Random.Range(30f, 60f);
        float elapsedTime = 0f;

        while (elapsedTime < spawnDuration)
        {
            Transform spawnPoint = Random.Range(0, 2) == 0 ? spawnRoof : spawnFloor;
            Instantiate(enemy, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnCoolDown);
            elapsedTime += spawnCoolDown;
        }

        StartCoroutine(MoveToFlee());
    }

    IEnumerator MoveToFlee()
    {
        yield return StartCoroutine(MoveToPosition(fleePosition.position, moveDuration));
        StartCoroutine(ZeplinCooldown());
    }

    IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        Vector3 startPos = transform.position;
        target = new Vector3(target.x, startPos.y, target.z); 

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            t = Mathf.SmoothStep(0, 1, t); 

            transform.position = Vector3.Lerp(startPos, target, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
    }

    IEnumerator ZeplinCooldown()
    {
        float cooldownTime = Random.Range(3f, 9f);
        yield return new WaitForSeconds(cooldownTime);
        CallZeplin();
    }
}
