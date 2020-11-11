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
    [SerializeField] float chaseRange;


    float attackTimer;
    Player player;

    EnemyState state = EnemyState.Idle;

    private void Start()
    {
        Setup(transform.position + healthOffset);
        attackTimer = Time.time;
    }

    private void Update()
    {
        if(player != null)
        {
            if (Time.time > attackTimer && PlayerInRange())
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

    private bool PlayerInRange()
    {
        return Vector2.Distance(player.transform.position, transform.position) < attackRange;
    }

    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(state == EnemyState.Idle)
        {
            player = collision.GetComponent<Player>();
        }
    }
}
