using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapons1 : Weapon
{
    public EnemyDamager damager;
    public Projectile projectile;

    private float shotCounter;
    public float weaponRange;
    public LayerMask whatIsEnemy;

    // How many times per second to perform the check
    public float checkFrequency;
    private float checkCounter;

    void Start()
    {
        SetStats();
    }

    void Update()
    {
        if (statsUpdated)
        {
            statsUpdated = false;
            SetStats();
        }

        shotCounter -= Time.deltaTime;
        checkCounter -= Time.deltaTime;

        if (checkCounter <= 0)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * stats[weaponLvl].range, whatIsEnemy);

            if (enemies.Length > 0)
            {
                Vector3 closestEnemy = GetClosestEnemy(enemies).transform.position;

                // Reset check counter
                checkCounter = 1f / checkFrequency;

                // If it's time to shoot, attack the nearest enemy and additional enemies randomly
                if (shotCounter <= 0)
                {
                    shotCounter = stats[weaponLvl].timeBetweenAttacks;

                    // Attack the nearest enemy
                    ShootProjectile(closestEnemy);

                    // Random attacks after the first one
                    for (int i = 1; i < stats[weaponLvl].amount; i++)
                    {
                        Vector3 randomEnemyPosition = enemies[Random.Range(0, enemies.Length)].transform.position;
                        ShootProjectile(randomEnemyPosition);
                    }
                }
            }
        }
    }

    Collider2D GetClosestEnemy(Collider2D[] enemies)
    {
        Collider2D closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    void ShootProjectile(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Instantiate(projectile, transform.position, rotation).gameObject.SetActive(true);
    }

    void SetStats()
    {
        damager.damageAmount = stats[weaponLvl].damage;
        damager.lifeTime = stats[weaponLvl].duration;
        damager.transform.localScale = Vector3.one * stats[weaponLvl].range;
        shotCounter = 0f;
        projectile.moveSpeed = stats[weaponLvl].speed;

        // Initialize the check counter
        checkCounter = 1f / checkFrequency;
    }
}
