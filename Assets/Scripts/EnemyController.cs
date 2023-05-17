using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    private Transform target;

    public float damage;

    public float hitWaitTime = 1f;
    private float hitCounter;

    public float health = 5f;

    public float knockBackTime = .5f;
    private float knockBackCounter;

    public float attackDistance = 1f;

    private EnemyAnimator enemyAnimator;

    public static EnemyController instance;

    private void Awake()
    {
        instance = this;
        enemyAnimator = GetComponent<EnemyAnimator>();
    }

    private void Start()
    {
        target = PlayerHealthController.instance.transform;
    }

    private void Update()
    {
        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;
            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f;
            }

            if (knockBackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * .5f);
            }
        }

        theRB.velocity = (target.position - transform.position).normalized * moveSpeed;

        if (hitCounter > 0f)
        {
            hitCounter -= Time.deltaTime;
        }

        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        enemyAnimator.SetAttackDistance(distanceToTarget <= attackDistance);
        enemyAnimator.SetMoveSpeed(moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && hitCounter <= 0f)
        {
            PlayerHealthController.instance.TakeDamage(damage);

            hitCounter = hitWaitTime;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;

        if (health <= 0)
        {
            Die();
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
    }

    public void TakeDamage(float damageToTake, bool shouldKnockback)
    {
        TakeDamage(damageToTake);

        if (shouldKnockback)
        {
            knockBackCounter = knockBackTime;
        }
    }

    private void Die()
    {
        enemyAnimator.PlayDeathAnimation();
        enemyAnimator.SpawnDeathEffect(transform.position);

        // Handle enemy death
    }
}
