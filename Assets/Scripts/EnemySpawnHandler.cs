using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnHandler : MonoBehaviour
{
    [SerializeField] GameObject[] spawnersPos;
    [SerializeField] EnemySpawner spawnerPref;

    List<EnemySpawner> spawners;

    [SerializeField] int numWaves;
    [SerializeField] int maxEnemies;

    // Start is called before the first frame update
    void Start()
    {
        spawners = new List<EnemySpawner>(spawnersPos.Length);
        foreach(var spawner in spawnersPos)
        {
            EnemySpawner enemysp = spawner.GetComponent<EnemySpawner>();
            enemysp.Setup(maxEnemies / spawnersPos.Length);
            spawners.Add(enemysp);
        }
        StartCoroutine(StartSpawns());
    }

    private IEnumerator StartSpawns()
    {
        float delayAfterFirst = 1f;
        for (int i = 0; i < numWaves; i++)
        {
            bool spawned = false;
            foreach (var spawner in spawners)
            {
                spawned = spawner.Spawn();
            }
            if (!spawned)
            {
                i--;
            }
            yield return new WaitForSeconds(delayAfterFirst);
            delayAfterFirst = 25f;
        }
    }
}
