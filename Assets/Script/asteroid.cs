using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 2f;
    public int size = 3; // 3 for big, 2 for medium, 1 for small
    public AudioClip bombSound; // Assign the bomb sound clip in the Inspector
    public AudioSource audioSource;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
        
        Vector2 targetPosition = GetRandomScreenPosition();
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            Destroy(gameObject); // Destroy the current asteroid
            GameManager.instance.IncreaseScore();
            audioSource.enabled = true; 
            audioSource.PlayOneShot(bombSound);
            
            if (size > 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject newAsteroid = Instantiate(gameObject);
                    newAsteroid.transform.localScale = transform.localScale / 2f;
                    newAsteroid.GetComponent<Asteroid>().size = size - 1;

                    // Ensure the new asteroid has a collider (box collider in this case)
                    BoxCollider2D collider = newAsteroid.AddComponent<BoxCollider2D>();

                    // Redirect movement towards a random position
                    Vector2 targetPosition = GetRandomScreenPosition();
                    Vector2 newDirection = (targetPosition - (Vector2)newAsteroid.transform.position).normalized;
                    newAsteroid.GetComponent<Rigidbody2D>().velocity = newDirection * speed;
                }
            }
        }
    }

    void OnBecameInvisible()
    {
        // Destroy the asteroid when it is no longer visible
        Destroy(gameObject);
    }

    Vector2 GetRandomScreenPosition()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Generate random coordinates within the screen boundaries
        Vector2 randomPosition = new Vector2(Random.Range(0f, screenWidth), Random.Range(0f, screenHeight));

        // Convert the random screen coordinates to world coordinates
        return Camera.main.ScreenToWorldPoint(randomPosition);
    }
}
