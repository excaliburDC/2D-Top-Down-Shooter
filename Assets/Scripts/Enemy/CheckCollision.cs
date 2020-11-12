
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
     
    }

    /// <summary>
    /// Checks Trigger Collsion
    /// </summary>
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

            Bullet b = col.gameObject.GetComponent<Bullet>();

            TakeDamage(b.bulletDamage);

            col.gameObject.SetActive(false);

          
        }
    }

    /// <summary>
    /// Function to deal damage to enemy
    /// </summary>
    /// <param name="damage">Amount of damage dealt by player</param>
    private void TakeDamage(int damage)
    {
        enemyHealth.Value -= damage;

        

        // check health
        if (enemyHealth.Value > 0)
        {
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
