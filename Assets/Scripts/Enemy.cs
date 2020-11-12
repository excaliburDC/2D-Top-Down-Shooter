using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CheckCollision))]
public class Enemy : MonoBehaviour
{

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float pointsValue;
    [SerializeField] protected int damage;

    protected Rigidbody2D rb2d;
    protected Health health;

    
    protected virtual void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
    }

    
    


}
