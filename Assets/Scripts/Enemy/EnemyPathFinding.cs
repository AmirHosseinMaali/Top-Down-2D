using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 1f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private KnockBack knockBack;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockBack = GetComponent<KnockBack>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Move();
        if (moveDir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveDir.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
    private void Move()
    {
        if (knockBack.GettingKnockBack == false)
        {
            rb.MovePosition(rb.position + moveDir * (enemySpeed * Time.fixedDeltaTime));
        }

    }
    public void MoveTo(Vector2 targetPos)
    {
        moveDir = targetPos;
    }
    public void StopMoving()
    {
        moveDir = Vector3.zero;
    }
}
