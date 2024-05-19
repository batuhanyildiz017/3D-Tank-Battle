using UnityEngine;

public class EnemyTank : MonoBehaviour
{
    // Düşmanın başlangıç canı
    public int maxHealth = 100;
    private int currentHealth;

    // Düşmanın hareket hızı
    public float moveSpeed = 3f;

    // Gövde ve kule dönüş hızları
    public float bodyTurnSpeed = 50f;
    public float towerTurnSpeed = 100f;

    // Ateş etme için gerekli değişkenler
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    private float nextFireTime = 0f;

    public float bulletSpeed = 20f;

    // Hedef oyuncu
    private Transform player;

    // AIDetector referansı
    public AIDetector aiDetector;

    // Rastgele gezinme için gerekli değişkenler
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    private float timer;

    // Rastgele hedef noktası
    private Vector3 wanderTarget;

    // Tankın Body ve Tower parçaları
    public Transform body;
    public Transform tower;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timer = wanderTimer;
        wanderTarget = GetRandomWanderTarget();
    }

    void Update()
    {
        if (aiDetector.TargetVisible)
        {
            MoveTowardsPlayer();
            RotateBodyTowardsPlayer();
            RotateTowerTowardsPlayer();

            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else
        {
            Wander();
        }
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = player.position - body.position;
            direction.y = 0; // Y eksenindeki hareketi engelle
            if (direction.magnitude > 2f) // Oyuncuya çok yaklaşıyorsa dur
            {
                direction = direction.normalized;
                body.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
        }
    }

    void RotateBodyTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = player.position - body.position;
            direction.y = 0; // Y eksenindeki hareketi engelle
            Quaternion rotation = Quaternion.LookRotation(direction);
            body.rotation = Quaternion.RotateTowards(body.rotation, rotation, bodyTurnSpeed * Time.deltaTime);
        }
    }

    void RotateTowerTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = player.position - tower.position;
            direction.y = 0; // Y eksenindeki hareketi engelle
            Quaternion rotation = Quaternion.LookRotation(direction);
            tower.rotation = Quaternion.RotateTowards(tower.rotation, rotation, towerTurnSpeed * Time.deltaTime);
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }
    }

    void Wander()
    {
        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            wanderTarget = GetRandomWanderTarget();
            timer = 0;
        }

        Vector3 direction = wanderTarget - body.position;
        direction.y = 0; // Y eksenindeki hareketi engelle
        if (direction.magnitude > 1f)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            body.rotation = Quaternion.RotateTowards(body.rotation, rotation, bodyTurnSpeed * Time.deltaTime);
            body.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    Vector3 GetRandomWanderTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += body.position;
        randomDirection.y = 0; // Y eksenindeki hareketi engelle
        return randomDirection;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
