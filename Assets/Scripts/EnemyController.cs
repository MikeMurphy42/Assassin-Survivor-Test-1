using System.Collections;
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
    

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;
            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f;

            }

            if (knockBackCounter <= 0) // Mathf.Abs makes it a Positvie Number beinhg multiplied
            {
                moveSpeed = Mathf.Abs(moveSpeed * .5f);
            }
        }

        theRB.velocity = (target.position -transform.position).normalized * moveSpeed;

        if (hitCounter > 0f)
        {
            hitCounter -= Time.deltaTime;
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitCounter <= 0f)
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
            gameObject.SetActive(false);
                EnemyPooler enemyPooler = FindObjectOfType<EnemyPooler>();
                if (enemyPooler != null)
                {
                    enemyPooler.DisableEnemy();
                    
                }
        }
        
        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
        
    }

    public void TakeDamage(float damageToTake, bool shouldKnockback)
    {
        TakeDamage(damageToTake);

        if (shouldKnockback == true)
        {
            knockBackCounter = knockBackTime;
        }
    }

}