using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    [SerializeField] Transform firepoint;
    [SerializeField] GameObject bulletPref;

    private float fireForce = 20f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }        
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPref, firepoint.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(firepoint.up * fireForce, ForceMode2D.Impulse);
    }
    
}
