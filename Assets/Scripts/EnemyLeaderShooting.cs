using UnityEngine;

public class EnemyLeaderShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint1;
    public Transform firePoint2;
    public float bulletSpeed = 5f;
    public float fireRate = 2f;

    private float nextFireTime = 0f;
    public AudioClip firingSound; 
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Fire();
        }
    }

    void Fire()
    {
        GameObject bullet1 = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
        rb1.velocity = firePoint1.up * bulletSpeed;

        GameObject bullet2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        rb2.velocity = firePoint2.up * bulletSpeed;
        
        if (audioSource != null && firingSound != null)
        {
            audioSource.PlayOneShot(firingSound);
        }
    }
}
