using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10.0f; // Adjust this value for bullet speed
    public float destroyDelay = 4.0f; // Adjust this value for the delay before the bullet is destroyed

    void Start()
    {
        // Apply an initial velocity to the bullet when it's instantiated
        Rigidbody2D bulletRb = GetComponent<Rigidbody2D>();
        bulletRb.velocity = transform.up * bulletSpeed;
        
        // Schedule the bullet for destruction after the specified delay
        Destroy(gameObject, destroyDelay);
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the bullet when it collides with something
        Destroy(gameObject);
    }
}