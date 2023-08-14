using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float changeRoamingDirectionTime = 2f;
    [SerializeField] float attackRange = 0;
    [SerializeField] float attackCD = 1f;
    [SerializeField] MonoBehaviour enemyType;

    private bool canAttack = true;
    private float timeRoaming = 0f;


    private Vector2 roamPosition;
    private State state;
    private EnemyPathFinding pathFinding;
    private enum State
    {
        Roaming,
        Attacking
    }



    private void Awake()
    {
        pathFinding = GetComponent<EnemyPathFinding>();
        state = State.Roaming;
    }
    private void Start()
    {
        roamPosition = GetRoamingPosition();
    }
    private void Update()
    {
        MovementStateControl();
    }
    private void MovementStateControl()
    {
        switch (state)
        {
            case State.Roaming:
                Roaming();
                break;
            case State.Attacking:
                Attacking();
                break;
        }
    }
    private void Roaming()
    {
        timeRoaming += Time.deltaTime;
        pathFinding.MoveTo(roamPosition);

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }
        if (timeRoaming > changeRoamingDirectionTime)
        {
            roamPosition = GetRoamingPosition();
        }
    }
    private void Attacking()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            state = State.Roaming;
        }
        if (attackRange != 0 && canAttack)
        {
            canAttack = false;
            (enemyType as IEnemy).Attack();
            StartCoroutine(AttackCooldownRoutine());
        }

    }
    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCD);
        canAttack = true;
    }
    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
