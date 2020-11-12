using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Asteroid : Enemy,IPooledObject
{
    [SerializeField] private float asteroidLifetime = 5f;

    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(DisableAsteroid());
    }

    
    public void OnObjectSpawner()
    {
        rb2d.velocity = -transform.up * moveSpeed;

    }

    private IEnumerator DisableAsteroid()
    {
        yield return new WaitForSeconds(asteroidLifetime);
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log(col.name);

            PlayerController p = col.GetComponent<PlayerController>();

            p.DestroyShip(damage);

        }

        if (col.gameObject.tag == "PlayerBullet")
        {
            Debug.Log(col.name);
        }
    }



}
