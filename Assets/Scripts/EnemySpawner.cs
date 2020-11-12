using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

class EnemySpawner: MonoBehaviour
{
    [SerializeField] Enemy enemyPref;
    [SerializeField] Vector2 maxSpawnVec;

    private int maxNumEnemies;

    List<Enemy> enemies;
    List<Vector2> spawns;
    public void Setup(int maxNumEnemies)
    {
        this.maxNumEnemies = maxNumEnemies;
        enemies = new List<Enemy>(maxNumEnemies);
        spawns = new List<Vector2>(maxNumEnemies);
        Random.InitState(System.DateTime.Now.Second);
        for (int i = 0; i < maxNumEnemies; i++)
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
        spawnerPos.x += Random.Range(-maxSpawnVec.x, maxSpawnVec.x);
        spawnerPos.y += Random.Range(-maxSpawnVec.y, maxSpawnVec.y);
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
                enemies[i].Revive(spawns[i]);
            }
        }
        return true;
    }
}

