﻿
using UnityEngine;

public class MoveCollectible : MonoBehaviour,IPooledObject
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb2d;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void OnObjectSpawner()
    {
        rb2d.velocity = -transform.up * moveSpeed;
    }

   
}
