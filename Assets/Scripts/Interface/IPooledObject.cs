
using UnityEngine;

public interface IPooledObject 
{
    /// <summary>
    /// Function to perform some action that can be implemented by other classes which inherits IPooledObject interface
    /// </summary>
    void OnObjectSpawner(); 
}
