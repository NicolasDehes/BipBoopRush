using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float baseSpeed = 5f;
    public float speedIncrease = 2f;
    public GameManager gameManager;

    private Rigidbody2D rb;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * currentSpeed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over: Hit an obstacle");
            gameManager.GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PointBox"))
        {
            // Ajouter des points
            gameManager.AddScore(10);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Electric"))
        {
            // Augmenter la vitesse et la surtension
            currentSpeed += speedIncrease;
            gameManager.IncreaseOvercharge(10f);
            // Destroy(collision.gameObject);

            if (gameManager.IsOvercharged())
            {
                Debug.Log("Game Over: Overcharged");
                gameManager.GameOver();
            }
        }
    }
}
