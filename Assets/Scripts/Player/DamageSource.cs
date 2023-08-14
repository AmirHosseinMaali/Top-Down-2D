using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private float damagePower ;
    private void Start()
    {
        MonoBehaviour currentActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
        damagePower = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        enemyHealth?.TakeDamage(damagePower);
    }
}
