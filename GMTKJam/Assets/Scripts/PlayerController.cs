using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ================= Variables =================
    [SerializeField] private float moveSpeed;

    // ================= Refrences =================
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GameObject interactSprite;
    private Rigidbody2D rb;
    private Vector2 movement;

    [SerializeField] private Interact interact;

    // ================= Events =================
    public delegate void PlayerAction();
    public static event PlayerAction OnSabotage;

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

        // Interact
        if (Input.GetKeyDown(KeyCode.E) && interact != null)
        {
            interact.InvokeEvent();
            OnSabotage();
        }
    }

    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            interactSprite.SetActive(true);
            interact = collision.GetComponent<Interact>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(interact != null)
        {
            interactSprite.SetActive(false);
            interact = null;
        }
    }
}
