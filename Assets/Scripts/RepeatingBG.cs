using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RepeatingBG : MonoBehaviour
{
    private BoxCollider2D spaceCollider;
    private float spaceVerticalLength; //value to store the vertical length of the gameobject's box collider


    void Awake()
    {
        spaceCollider = GetComponent<BoxCollider2D>();

        spaceVerticalLength = spaceCollider.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        //if y position of the BG is less than its collider's length (-ve)
        //then call RepostionBackground()
        if(transform.position.y <  -spaceVerticalLength)
        {
            RepositionBackground();
        }
    }

    /// <summary>
    /// Function to reposition the background if it goes out of view
    /// </summary>
    private void RepositionBackground()
    {
        Vector2 offset = new Vector2(0, spaceVerticalLength * 2f);
        transform.position = (Vector2)transform.position + offset;
    }
}
