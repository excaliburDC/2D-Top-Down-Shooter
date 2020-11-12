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


    



}
