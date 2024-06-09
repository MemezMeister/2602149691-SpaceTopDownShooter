using UnityEngine;

public class PlayerFiring : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 1f;
    public int damage = 1;
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
            Fire();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = damage;
        }
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed;
        if (audioSource != null && firingSound != null)
        {
            audioSource.PlayOneShot(firingSound);
        }
    }

    public void IncreaseFireRate(float amount)
    {
        fireRate = Mathf.Max(0.1f, fireRate - amount); 
    }
}