using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ScrollingBG : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 2f;

    private Rigidbody2D rb2D;

 
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        BeginScrolling();
    }

    /// <summary>
    /// Begins the scrolling of the background to give the game a movement effect
    /// </summary>
    private void BeginScrolling()
    {
        rb2D.velocity = new Vector2(0f, -scrollSpeed);
    }
}
