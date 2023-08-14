using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float restoreDefaultTime = .2f;
    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth;
    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }
    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material=whiteFlashMat;
        yield return new WaitForSeconds(restoreDefaultTime);
        spriteRenderer.material=defaultMat;
        if (enemyHealth != null )
        {
            enemyHealth.EnemyDeath();
        }
        
    }
}
