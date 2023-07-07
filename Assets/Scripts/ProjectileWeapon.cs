using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileWeapon : Weapon
{

    public EnemyDamager damager;
    public Projectile projectile;

    private float shotCounter;
    public float weaponRange;
    public LayerMask whatIsEnemy;
    
    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (statsUpdated == true)
        {
            statsUpdated = false;
            SetStats();
        }

        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            shotCounter = stats[weaponLvl].timeBetweenAttacks;

            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * stats[weaponLvl].range, whatIsEnemy);

            if (enemies.Length > 0)
            {
                for (int i = 0; i < stats[weaponLvl].amount; i++)
                {
                    Vector3 targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;

                    Vector3 direction = targetPosition - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    //Test to see if the angle is right. below is the test number
                    angle -= 90;
                    projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    
                    Instantiate(projectile, projectile.transform.position, projectile.transform.rotation).gameObject.SetActive(true);
                }
            }
            
        }
    }

    void SetStats()
    {
        damager.damageAmount = stats[weaponLvl].damage;
        damager.lifeTime = stats[weaponLvl].duration;
        
        damager.transform.localScale = Vector3.one * stats[weaponLvl].range;

        shotCounter = 0f;
        projectile.moveSpeed = stats[weaponLvl].speed;

    }
    
}
