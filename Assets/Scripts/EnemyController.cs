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
    
    //public EnemyPooler enemyPooler; // Reference to EnemyPooler script
    public GameObject enemyPrefab;
    //public int activeEnemyCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //enemyPooler = FindObjectOfType<EnemyPooler>(); // Find and store reference to EnemyPooler script
        //target = FindObjectOfType<PlayerController>().transform;
        target = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = (target.position -transform.position).normalized * moveSpeed;

        if (hitCounter > 0f)
        {
            hitCounter -= Time.deltaTime;
            //activeEnemyCount--;
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
            //public void Disable()
            //{
                gameObject.SetActive(false);
                EnemyPooler enemyPooler = FindObjectOfType<EnemyPooler>();
                if (enemyPooler != null)
                {
                    enemyPooler.DisableEnemy();
                }
            //}
            //if (gameObject.CompareTag("Enemy"))
            //{
            //gameObject.SetActive(false);
            //}
            // Deactivate an enemy and decrease activeEnemyCount
            
            enemyPrefab.SetActive(false);
            //activeEnemyCount--;
        }
    }
}