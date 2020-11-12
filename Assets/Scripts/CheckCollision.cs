using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    private Enemy enemy;
    private Health enemyHealth;
    
    private int initialHealth;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyHealth = GetComponent<Health>();

        initialHealth = enemyHealth.Value;
        //Debug.Log(enemy.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log(col.name);

            PlayerController p = col.GetComponent<PlayerController>();

            p.DestroyShip(enemy.Damage);

            TakeDamage(enemy.Damage);

        }

        if (col.gameObject.tag == "PlayerBullet")
        {
            Debug.Log(col.name);

            Bullet b = col.gameObject.GetComponent<Bullet>();

            TakeDamage(b.bulletDamage);

            //enemy.health -= 10;
            //Debug.Log(enemy.health);

            //if(enemy.health < 0)
            //{
            //    enemy.health = 10;
            //    gameObject.SetActive(false);
            //}
        }
    }

    private void TakeDamage(int damage)
    {
        enemyHealth.Value -= damage;

        //AudioManager.Instance.PlaySFX(collisionSound);

        // check health
        if (enemyHealth.Value > 0)
        {
            Debug.Log(enemyHealth.Value);
            // flashColor.Flash();
        }
        else
        {
            // particles
            // GameObject particles = Instantiate(explosionParticlesPrefab, transform.position, Quaternion.identity) as GameObject;
            // Destroy(particles, 1.0f);
            gameObject.SetActive(false);
            enemyHealth.Value = initialHealth;

        }
    }
}
