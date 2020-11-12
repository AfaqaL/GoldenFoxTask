using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    enum EnemyState
    {
        Idle, Chase
    }

    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float escapeRadius;
    [SerializeField] AIPath finder;
    [SerializeField] AIDestinationSetter target;

    float attackTimer;
    Player player;
    EnemyState state = EnemyState.Idle;
    
    private void Start()
    {
        Setup(transform.position + healthOffset);
        attackTimer = Time.time;

        if (!finder.isActiveAndEnabled)
        {
            Debug.Log("Finder is not actie yet");
        }
    }

    private void Update()
    {
        healthBar.Follow(transform.position + healthOffset);
        if(state == EnemyState.Chase)
        {
            float distance = Vector2.Distance(player.transform.position, transform.position);
            if (distance > escapeRadius)
            {
                finder.enabled = false;
                state = EnemyState.Idle;
            }
            else if (Time.time > attackTimer &&  distance < attackRange)
            {
                player.TakeDamage(damage);
                attackTimer = Time.time + 0.5f/attackSpeed;
                if (!player.isActiveAndEnabled)
                {
                    player = null;

                }
            }
            
        }
    }

    public void Revive(Vector3 pos)
    {
        transform.position = pos;
        Setup(pos + healthOffset);
        gameObject.SetActive(true);
        healthBar.SetHealth(maxHealth);
        healthBar.gameObject.SetActive(true);
    }

    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("detecting selfsss");
        if(state == EnemyState.Idle)
        {
            player = collision.GetComponent<Player>();
            if (player != null)
            {
                target.target = player.transform;
                finder.enabled = true;
                state = EnemyState.Chase;
            }
        }
    }
}
