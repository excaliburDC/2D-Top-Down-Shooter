
using UnityEngine;

public class FireBullets : MonoBehaviour
{

    [SerializeField] private float fireRate = 1f;


    private Transform spawnPoint;

    private float fireCountdown;

    
    void Awake()
    {
        spawnPoint = gameObject.transform.GetChild(0); //Spawnpoint is the first child object of the current gameobject
    }

    // Update is called once per frame
    void Update()
    {
        FireEnemyBullet();
    }

    /// <summary>
    /// Enemy fires bullet in intervals
    /// </summary>
    private void FireEnemyBullet()
    {
        if (fireCountdown < 0f)
        {

            PoolManager.Instance.SpawnInWorld("EnemyBullet", spawnPoint.position, spawnPoint.rotation);
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }
}
