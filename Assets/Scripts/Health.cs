using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{

    public event Action<int> OnHealthChange;

    [SerializeField]
    private int health = 1;
    public int Value
    {
        get { return health; }
        set { health = value; }
    }

  

    /// <summary>
    /// Reduces the health.
    /// </summary>
    /// <param name="value">The value.</param>
    public void ReduceHealth(int value)
    {
        Value -= value;

        if (OnHealthChange != null)
        {
            OnHealthChange(Value);
        }

        
    }

    
}
