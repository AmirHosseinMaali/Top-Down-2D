using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float shootSpeed = 22f;
    [SerializeField] GameObject particleOnHitVFX;
    [SerializeField] bool isEnemyProjectile = false;
    [SerializeField] float projectileRange = 10f;

    Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }
    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }
    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }
    public void UpdateProjectileSpeed(float shootSpeed)
    {
        this.shootSpeed = shootSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = collision.gameObject.GetComponent<Indestructible>();
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        if (!collision.isTrigger && (enemyHealth || indestructible || player))
        {
            if ((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
            {
                player?.TakeDamage(1, transform);
                Instantiate(particleOnHitVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if (!collision.isTrigger && indestructible)
            {
                Instantiate(particleOnHitVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > projectileRange)
        {
            Destroy(gameObject);
        }
    }
    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * shootSpeed);
    }
}
