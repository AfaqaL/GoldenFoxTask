using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMecanics : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] Rigidbody2D p_rb;
    [SerializeField] Camera cam;

    private Vector2 movement;
    private Vector2 mousePos;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        p_rb.MovePosition(p_rb.position + movement * moveSpeed * Time.deltaTime);

        Vector2 direction = mousePos - p_rb.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        p_rb.rotation = angle;
    }


}
