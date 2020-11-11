using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour,IPooledObject
{
    [SerializeField] private float bulletSpeed = 4f;
    
    private Rigidbody2D rb2d; 

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>(); 
    }

    public void OnObjectSpawner()
    {

        rb2d.velocity = transform.up * bulletSpeed;

        
    }

    
}
