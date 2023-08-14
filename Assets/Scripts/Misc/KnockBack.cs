using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool GettingKnockBack { get; private set; }
    [SerializeField] private float knockBackDuration=.2f;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        GettingKnockBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }
    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackDuration);
        rb.velocity = Vector2.zero;
        GettingKnockBack=false;
    }
}
