using System.Collections;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private bool x;
    [SerializeField]
    private bool y;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform target;

    private IEnumerator Start()
    {
        while (true)
        {
            if (target != null && target.position != transform.position)
            {
                Vector3 start = transform.position;
                Vector3 end = new Vector3(x ? target.position.x : transform.position.x, y ? target.position.y : transform.position.y, transform.position.z);

                float timer = 0f;

                while (timer < 1f)
                {
                    transform.position = Vector3.Lerp(start, end, timer);

                    timer += speed * Time.deltaTime;

                    yield return null;
                }
            }

            yield return null;
        }
    }
}
