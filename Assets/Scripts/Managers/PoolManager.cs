﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolObj
{
    public string poolName;
    public GameObject poolPrefab;
    public int size;
}

public class PoolManager : SingletonManager<PoolManager>
{
    public List<PoolObj> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    


    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (PoolObj pool in pools)
        {
            Queue<GameObject> objectToPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject gObj = Instantiate(pool.poolPrefab);
                gObj.SetActive(false);
                gObj.transform.parent = this.transform;
                objectToPool.Enqueue(gObj);

            }

            poolDictionary.Add(pool.poolName, objectToPool);
        }

    }

    /// <summary>
    /// Spawns Gameobject from the Object Pool in the world
    /// </summary>
    /// <param name="tag">Name of the Gameobject to spawn</param>
    /// <param name="position">Position where the gameobject should spawn</param>
    /// <param name="rotation">Rotation of the gameobject</param>
    /// <returns></returns>
    public GameObject SpawnInWorld(string tag,Vector3 position,Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError("Object with tag " + tag + " not found...");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        

        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawner();
        }


        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
