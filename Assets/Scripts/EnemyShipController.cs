using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : Enemy,IPooledObject
{
    
    [SerializeField] private float fireRate = 1f;


    private Transform spawnPoint;
   
    private float fireCountdown;


    protected override void OnEnable()
    {
        base.OnEnable();
        spawnPoint = gameObject.transform.GetChild(0); //Spawnpoint is the first child object of the current gameobject
    }

  

    public void OnObjectSpawner()
    {
        MoveEnemyShip();
        
    }

   
    private void MoveEnemyShip()
    {
        rb2d.velocity = -transform.up * moveSpeed;
    }
    private void FireEnemyBullet()
    {
        if (fireCountdown < 0f)
        {

            PoolManager.instance.SpawnInWorld("PlayerBullet", spawnPoint.position, spawnPoint.rotation);
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    
}
