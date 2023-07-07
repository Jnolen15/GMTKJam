using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ================= Variables =================
    [SerializeField] private float moveSpeed;

    // ================= Refrences =================
    [SerializeField] private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Vector2 movement;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Calulate movement
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement.Normalize();

        // Flip sprite
        if (movement.x > 0.1)
            sprite.flipX = true;
        else if (movement.x < -0.1)
            sprite.flipX = false;
    }

    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
