using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlashColor))]
public class CheckCollision : MonoBehaviour
{
    private Enemy enemy;
    private Health enemyHealth;
    private FlashColor flashColor;
    
    private int initialHealth;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyHealth = GetComponent<Health>();
        flashColor = GetComponent<FlashColor>();

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

            col.gameObject.SetActive(false);

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
            flashColor.Flash();
        }
        else
        {
            // particles
            GameObject particles = Instantiate(enemy.explosionPrefab, transform.position, Quaternion.identity);
            Destroy(particles, 0.5f);
            GameController.Instance.UpdatePointsValue(enemy.PointsValue);
            gameObject.SetActive(false);
            enemyHealth.Value = initialHealth;

        }
    }
}
