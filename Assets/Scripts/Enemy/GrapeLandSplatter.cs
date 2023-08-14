using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeLandSplatter : MonoBehaviour
{
    SpriteFade spriteFade;
    private void Awake()
    {
        spriteFade = GetComponent<SpriteFade>();
    }
    private void Start()
    {
        StartCoroutine(spriteFade.SlowFadeRoutine());
        Invoke("DisableCollider", .2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth=collision.gameObject.GetComponent<PlayerHealth>();

        playerHealth?.TakeDamage(1, transform);
    }
    private void DisableCollider()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
