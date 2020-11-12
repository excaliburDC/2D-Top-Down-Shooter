using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomRandomGenExtension;


public class EnemyManager : MonoBehaviour
{
    public List<string> enemies;
    public List<Transform> enemySpawnPoints;

	[SerializeField] private float spawnRate = 0.5f;

	private Camera cam;
	private int currentStringIndex;
    private int currentTransformIndex;
	private float timeSinceLastEnemySpawned;

    

    // Start is called before the first frame update
    void Start()
    {
		cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
		StartCoroutine(SpawnEnemies());
    }

    /// <summary>
    /// Spawns Random enemy from pool after a particular time
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(2f);

        currentStringIndex = RandomIndexGenerator.GetRandomIndex<string>(enemies);
        currentTransformIndex = RandomIndexGenerator.GetRandomIndex<Transform>(enemySpawnPoints);

        


        if (timeSinceLastEnemySpawned<0f)
        {

			PoolManager.Instance.SpawnInWorld(enemies[currentStringIndex],enemySpawnPoints[currentTransformIndex].position,enemySpawnPoints[currentTransformIndex].rotation );

			timeSinceLastEnemySpawned = 1f / spawnRate;
		}

		timeSinceLastEnemySpawned -= Time.deltaTime;

        yield return null;

        
    }

	

	


}
