using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();

        Debug.Log(enemy.gameObject.name);
    }
}
