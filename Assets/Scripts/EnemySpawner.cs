using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

class EnemySpawner: MonoBehaviour
{
    [SerializeField] Enemy enemyPref;

    private int maxSpawns = 16;
    private float maxSpawnRadius = 3f;

    List<Enemy> enemies = new List<Enemy>(16);
    List<Vector2> spawns = new List<Vector2>(16);
    private void Start()
    {
        for (int i = 0; i < maxSpawns; i++)
        {
            Vector2 spawnPoint = RandomSpawnPoint();
            spawns.Add(spawnPoint);
            Enemy enemy = Instantiate(enemyPref, spawnPoint, Quaternion.identity);
            enemy.gameObject.SetActive(false);
            enemy.isDead = true;           
            enemies.Add(enemy);
        }
    }

    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnerPos = gameObject.transform.position;
        spawnerPos.x += Random.Range(-maxSpawnRadius, maxSpawnRadius);
        spawnerPos.y += Random.Range(-maxSpawnRadius, maxSpawnRadius);
        return spawnerPos;
    }

    public bool Spawn()
    {
        bool noneDead = true;
        foreach (var enemy in enemies)
        {
            if (enemy.isDead)
            {
                noneDead = false;
            }
        }
        if (noneDead)
        {
            return false;
        }
        
        for(int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].isActiveAndEnabled && Random.Range(0, 3) == 1)
            {
                if (!enemies[i].transform.position.Equals(spawns[i]))
                {
                    enemies[i].transform.position = spawns[i];
                }
                enemies[i].gameObject.SetActive(true);
                enemies[i].isDead = false;
            }
        }
        return true;
    }
}

