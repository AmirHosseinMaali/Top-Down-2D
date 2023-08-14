using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    enum PickUpType
    {
        GoldCoin,
        StaminaGlobe,
        HealthGlobe
    }

    [SerializeField] PickUpType type;
    [SerializeField] float pickUpDistance = 2f;
    [SerializeField] float accelerationRate = .2f;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] AnimationCurve animCurve;
    [SerializeField] float heightY = 1.5f;
    [SerializeField] float popDuration = 1f;

    Vector3 moveDir;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        StartCoroutine(AnimCurveSpawnRoutine());
    }
    private void Update()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;
        if (Vector3.Distance(transform.position, playerPos) < pickUpDistance)
        {
            moveDir = (playerPos - transform.position).normalized;
            moveSpeed += accelerationRate;
        }
        else
        {
            moveDir = Vector3.zero;
            moveSpeed = 0;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            DetectPickUpType();
            Destroy(this.gameObject);
        }
    }
    private IEnumerator AnimCurveSpawnRoutine()
    {
        float timePassed = 0;
        Vector2 startPoint = transform.position;
        Vector2 endPoint = new Vector2(startPoint.x + Random.Range(-2, 2), startPoint.y + Random.Range(-1, 1));


        while (timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0, heightY, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0, height);

            yield return null;
        }
    }
    private void DetectPickUpType()
    {
        switch (type)
        {
            case PickUpType.GoldCoin:
                EconomyManager.Instance.UpdateCurrentGold();
                break;
            case PickUpType.StaminaGlobe:
                PlayerStamina.Instance.RefreshStamina();
                break;
            case PickUpType.HealthGlobe:
                PlayerHealth.Instance.HealPlayer();
                break;
        }
    }
}
