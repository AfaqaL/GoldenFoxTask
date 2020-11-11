using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Player : Character
{
    

 
    private void Start()
    {
        Setup(transform.position + healthOffset);
    }

    private void Update()
    {
        healthBar.transform.position = transform.position + healthOffset;
    }

    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
    }
}
