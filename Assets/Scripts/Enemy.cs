using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float pointsValue;

    protected Rigidbody2D rb2d;

    
    protected virtual void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

}
