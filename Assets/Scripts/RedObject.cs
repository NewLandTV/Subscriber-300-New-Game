using System.Collections;
using UnityEngine;

public class RedObject : MonoBehaviour
{
    public new Transform transform;

    private float moveSpeed;

    private Transform target;

    private Vector3[] redObjectRandomPositions;

    private WaitForSeconds waitTime5f;

    private void Awake()
    {
        waitTime5f = new WaitForSeconds(5f);
    }

    private void OnEnable()
    {
        StartCoroutine(Start());
    }

    private IEnumerator Start()
    {
        Vector3 direction = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y, transform.position.z).normalized;

        StartCoroutine(Disable());

        while (true)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;

            yield return null;
        }
    }

    private void OnDisable()
    {
        StopCoroutine(Disable());

        transform.position = redObjectRandomPositions[Random.Range(0, redObjectRandomPositions.Length)];
    }

    public void Initialize(Transform target, float moveSpeed, Vector3[] redObjectRandomPositions)
    {
        this.target = target;
        this.moveSpeed = moveSpeed;
        this.redObjectRandomPositions = redObjectRandomPositions;
    }

    private IEnumerator Disable()
    {
        yield return waitTime5f;

        gameObject.SetActive(false);
    }
}
