using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject collAnimation;
    int minDmg = 23;
    int maxDmg = 27;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // failed attempt :/ :D
        //if (!collision.collider.CompareTag("River"))
        //{
            GameObject animation = Instantiate(collAnimation, transform.position, Quaternion.identity);

            Enemy enemy = collision.collider.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.TakeDamage(Random.Range(minDmg, maxDmg));
            }

            Destroy(animation, 1.5f);
            Destroy(gameObject);
        //}
    }
}
