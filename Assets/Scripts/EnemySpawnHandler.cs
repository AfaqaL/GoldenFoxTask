using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnHandler : MonoBehaviour
{
    [SerializeField] GameObject[] spawnersPos;
    [SerializeField] EnemySpawner spawnerPref;

    List<EnemySpawner> spawners = new List<EnemySpawner>(6);
    private int numWaves = 4;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var spawner in spawnersPos)
        {
            spawners.Add(Instantiate(spawnerPref, spawner.transform.position, Quaternion.identity));
        }
        StartCoroutine(StartSpawns(numWaves));
    }

    private IEnumerator StartSpawns(int waves)
    {
        float delayAfterFirst = 1f;
        for (int i = 0; i < waves; i++)
        {
            bool spawned = false;
            foreach (var spawner in spawners)
            {
                spawned = spawner.Spawn();
            }
            yield return new WaitForSeconds(delayAfterFirst);
            delayAfterFirst = 25f;
        }
    }
}
