using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool forwardInput;
    private bool leftInput;
    private bool rightInput;
    private bool shootInput;
    public float speed = 10;
    public GameObject bulletPrefab; // Reference to your bullet prefab
    public Transform firePoint; // Point where the bullet will be generated
    public float bulletSpeed = 10.0f; // Adjust this value for bullet speed
    public float shootCooldown = 0.5f; // Adjust this value for the cooldown time
    private float cooldownTimer;
    public GameManager gameManager;
    public AudioClip spaceshipSound; // Reference to your spaceship sound
    private AudioSource audioSource;
    public AudioClip shootingSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent < Rigidbody2D>();
         audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (gameManager.getPlay()){
        forwardInput = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        leftInput = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        rightInput = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        shootInput = Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space);
        }
        
    }

    void FixedUpdate()
    {
        if (forwardInput)
        {
            rb.AddForce((Vector2)transform.up * speed);
        }
        if (leftInput)
        {
            rb.AddTorque(1);
        }
        if (rightInput)
        {
            rb.AddTorque(-1);
        }
        if (shootInput && cooldownTimer <= 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            cooldownTimer = shootCooldown;
            if (shootingSound != null)
            {
                audioSource.PlayOneShot(shootingSound);
            }
        }


        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        CheckScreenBoundaries();
        float spaceshipSpeed = rb.velocity.magnitude;

        // Play spaceship sound when moving
        if (spaceshipSpeed > 0.1f && !audioSource.isPlaying && spaceshipSound != null)
        {
            audioSource.loop = true;
            audioSource.clip = spaceshipSound;
            audioSource.Play();
        }
        // Stop spaceship sound when not moving
        else if (audioSource.isPlaying && spaceshipSpeed <= 0.1f )
        {
            audioSource.Stop();
        }
    }
   void CheckScreenBoundaries()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        viewportPos.z = 1f; // Ensure Z stays at zero
        
        if (viewportPos.x > 1.0f)
        {
            viewportPos.x = 0.0f;
        }
        else if (viewportPos.x < 0.0f)
        {
            viewportPos.x = 1.0f;
        }

        if (viewportPos.y > 1.0f)
        {
            viewportPos.y = 0.0f;
        }
        else if (viewportPos.y < 0.0f)
        {
            viewportPos.y = 1.0f;
        }

        transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
    }
      void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            gameManager.SubtractLife();
            StartCoroutine(ResetPlayer());
            DestroyAllAsteroids();
        }
    }

    IEnumerator ResetPlayer()
    {
        returnOriginalPosition();
        yield return new WaitForSeconds(1.0f); // Wait for 1 second before allowing further collisions
    }

    void DestroyAllAsteroids()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject asteroid in asteroids)
        {
            Destroy(asteroid);
        }
    }

    public void returnOriginalPosition()
    {
        transform.position = Vector3.zero;
        rb.velocity = Vector2.zero;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rb.angularVelocity = 0f;
    }
}



