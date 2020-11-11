using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Asteroid : Enemy,IPooledObject
{
    private float direction = 0f;

    // Start is called before the first frame update
    protected override void OnEnable()
    {
        base.OnEnable();
    }


    public void OnObjectSpawner()
    {
        if (direction == 0f)
        {
            // if no direction choose a random direction to move in.
            direction = Mathf.Floor(Random.Range(0.0f, 360.0f));
        }
        Vector3 rotation = new Vector3(0.0f, 0.0f, direction);
        transform.rotation = Quaternion.Euler(rotation);
    }


    
}
