using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log(col.name);
        }

        if(col.gameObject.tag == "PlayerBullet")
        {
            Debug.Log(col.name);
        }
    }
}
