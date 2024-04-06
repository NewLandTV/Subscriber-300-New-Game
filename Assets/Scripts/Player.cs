using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public new Transform transform;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int maxHealth;
    private int currentHealth;

    private bool moveable = true;

    [SerializeField]
    private Image healthBarImage;
    [SerializeField]
    private Gradient healthBarImageGradient;

    [SerializeField]
    private GameManager gameManager;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private IEnumerator Start()
    {
        while (true)
        {
            Move();

            float healthBar = (float)currentHealth / maxHealth;

            healthBarImage.fillAmount = healthBar;
            healthBarImage.color = healthBarImageGradient.Evaluate(healthBar);

            yield return null;
        }
    }

    private void Move()
    {
        if (moveable)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            transform.position += new Vector3(x, y, 0f).normalized * moveSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        int collisionLayer = collision.gameObject.layer;

        // Red Object
        if (collisionLayer == 9)
        {
            collision.gameObject.SetActive(false);

            currentHealth--;

            if (currentHealth <= 0)
            {
                moveable = false;

                gameManager.GameOver();
            }
        }
    }
}
