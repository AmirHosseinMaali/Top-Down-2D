using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLaser;
    [SerializeField] private Transform magicLaserSpawnPoint;

    private Animator animator;


    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        MouseFollowOffset();
    }
    public void Attack()
    {
        animator.SetTrigger(FIRE_HASH);
    }
    public void SpawnStaffProjectile()
    {
        GameObject newMagicLaser = Instantiate(magicLaser, magicLaserSpawnPoint.position, Quaternion.identity);
        newMagicLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
    }
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    private void MouseFollowOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerPos.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 180, angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);

        }
    }
}
