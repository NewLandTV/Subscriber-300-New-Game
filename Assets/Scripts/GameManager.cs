using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Red Object
    private List<RedObject> redObjectPooling = new List<RedObject>();
    [SerializeField]
    private GameObject redObjectPrefab;
    [SerializeField]
    [Range(10, 1000)]
    private int redObjectMakeCount;
    [SerializeField]
    private Transform redObjectZone;
    [SerializeField]
    private Vector3[] redObjectRandomPositions;
    private int redObjectCursor;

    [SerializeField]
    private Player player;
    [SerializeField]
    private GameObject playerDeadParticle;

    [SerializeField]
    private Canvas deadUICanvas;

    private WaitForSeconds waitTime2f;

    private void Awake()
    {
        for (int i = 0; i < redObjectMakeCount; i++)
        {
            MakeRedObject();
        }

        waitTime2f = new WaitForSeconds(2f);
    }

    private IEnumerator Start()
    {
        while (true)
        {
            redObjectPooling[redObjectCursor].gameObject.SetActive(true);

            redObjectCursor = (redObjectCursor + 1) % redObjectPooling.Count;

            yield return waitTime2f;
        }
    }

    public void MakeRedObject()
    {
        GameObject instance = Instantiate(redObjectPrefab, redObjectRandomPositions[Random.Range(0, redObjectRandomPositions.Length)], Quaternion.identity, redObjectZone);

        RedObject redObjectComponent = instance.GetComponent<RedObject>();

        redObjectComponent.Initialize(player.transform, Random.Range(6f, 16f), redObjectRandomPositions);

        redObjectPooling.Add(redObjectComponent);
    }

    public void GameOver()
    {
        deadUICanvas.gameObject.SetActive(true);

        player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<CircleCollider2D>().enabled = false;

        playerDeadParticle.transform.position = player.transform.position;

        playerDeadParticle.SetActive(true);
    }

    public void OnRetryButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
