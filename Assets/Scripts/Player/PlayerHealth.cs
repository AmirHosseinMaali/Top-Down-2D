using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] float maxHealth = 3;
    [SerializeField] float knockBackThrustAmount = 10f;
    [SerializeField] float damageRecoveryTime = .5f;

    float currentHealth;
    bool canTakeDamage = true;

    Slider healthSlider;
    KnockBack knockBack;
    Flash flash;


    const string HEALTH_SLIDER_TEXT = "HealthSlider";

    protected override void Awake()
    {
        base.Awake();
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
    }


    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthSlider();
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
        if (enemy)
        {
            TakeDamage(1, collision.transform);
        }
    }


    public void HealPlayer()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            UpdateHealthSlider();
        }
    }


    public void TakeDamage(float damage, Transform hitTransform)
    {
        if (!canTakeDamage)
        {
            return;
        }
        ScreenShakeManager.Instance.ShakeScreen();
        knockBack.GetKnockedBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damage;
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckPlayerDeath();
    }


    private void CheckPlayerDeath()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("U Died");
        }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }


    private void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
        }
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}
