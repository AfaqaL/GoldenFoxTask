using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject collAnimation;
    int damage = 20;

    void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject animation = Instantiate(collAnimation, transform.position, Quaternion.identity);

        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(animation, 1.5f);
        Destroy(gameObject);
    }
}
