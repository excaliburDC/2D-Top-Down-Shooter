using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScrollingBG : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.1f;

    private MeshRenderer meshRenderer;

    private float yScrollSpeed;

 
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
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
        yScrollSpeed = Time.time * scrollSpeed;

        Vector2 offset = new Vector2(0f, yScrollSpeed);

        meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
