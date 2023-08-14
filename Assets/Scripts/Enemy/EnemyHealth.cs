using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float startHealth = 3;
    [SerializeField] private float currentHealth;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] float knockBackThrust = 15f;
    private KnockBack knockBack;
    private Flash flash;
    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
    }
    private void Start()
    {
        currentHealth = startHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        knockBack.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(flash.FlashRoutine());
    }
    public void EnemyDeath()
    {
        if (currentHealth <= 0)
        {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            GetComponent<PickUpSpawner>().DropItems();
            Destroy(gameObject);
        }
    }
}
