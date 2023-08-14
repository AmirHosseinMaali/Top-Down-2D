using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletMoveSpeed;
    [SerializeField] int burstCount;
    [SerializeField] int projectilesPerBurst;
    [SerializeField] float timeBetweenBursts;
    [SerializeField] float startDistance = .1f;
    [SerializeField] float restTime = 1f;
    [SerializeField][Range(0, 359)] float angleSpread;
    [SerializeField] bool stagger;
    [SerializeField] bool oscillate;

    bool isShooting = false;

    private void OnValidate()
    {
        if (oscillate) { stagger = true; }
        if (!oscillate) { stagger = false; }
        if (projectilesPerBurst < 1) { projectilesPerBurst = 1; }
        if (burstCount < 1) { burstCount = 1; }
        if (timeBetweenBursts < .1f) { timeBetweenBursts = .1f; }
        if (restTime < .1f) { restTime = .1f; }
        if (startDistance < .1f) { startDistance = .1f; }
        if (angleSpread == 0) { projectilesPerBurst = 1; }
        if (bulletMoveSpeed <= 0) { bulletMoveSpeed = .1f; }
    }
    public void Attack()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }
    private IEnumerator ShootRoutine()
    {
        isShooting = true;

        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;

        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

        if (stagger) { timeBetweenProjectiles = timeBetweenBursts / projectilesPerBurst; }

        for (int i = 0; i < burstCount; i++)
        {
            if (!oscillate)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }
            if (oscillate && i % 2 != 1)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }
            else if (oscillate)
            {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }
            for (int j = 0; j < projectilesPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);
                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateProjectileSpeed(bulletMoveSpeed);
                }
                currentAngle += angleStep;
                if (stagger)
                {
                    yield return new WaitForSeconds(timeBetweenProjectiles);
                }
            }
            currentAngle = startAngle;
            if (!stagger)
            {
                yield return new WaitForSeconds(timeBetweenBursts);
            }
        }


        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0;
        angleStep = 0;
        if (angleSpread != 0)
        {
            angleStep = angleSpread / (projectilesPerBurst - 1);
            halfAngleSpread = angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + startDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}
