using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : Enemy,IPooledObject
{
    
    


    protected override void OnEnable()
    {
        base.OnEnable();
       
    }



    public void OnObjectSpawner()
    {
        MoveEnemyShip();
        
    }
   
    private void MoveEnemyShip()
    {
        rb2d.velocity = -transform.up * moveSpeed;
    }

    

    
}
