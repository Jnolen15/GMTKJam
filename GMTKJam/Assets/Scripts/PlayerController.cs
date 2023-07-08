using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // ================= Variables =================
    [SerializeField] private float moveSpeed;
    [SerializeField] private float curInteract;

    // ================= Refrences =================
    [SerializeField] private Image interactSprite;
    [SerializeField] private Image interactSpriteGhost;
    [SerializeField] private GameObject interactSpriteCDIndicator;
    [SerializeField] private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Vector2 movement;

    [SerializeField] private Interact interact;

    // ================= Events =================
    public delegate void PlayerAction();
    public static event PlayerAction OnSabotage;
    public static event PlayerAction OnActNormal;

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
        if (Input.GetKey(KeyCode.E) && interact != null && !interact.onCD)
        {
            // Hold to progress interaction
            if(curInteract < interact.interactTime)
            {
                curInteract += Time.deltaTime * 1;
                interactSprite.fillAmount = (curInteract / interact.interactTime);
            }
            // Complete interaction
            else
            {
                interact.InvokeEvent();

                if (interact.isSabotage)
                    OnSabotage();
                else
                    OnActNormal();

                curInteract = 0;
                interactSprite.fillAmount = 0;
            }
        }

        // CD indicator
        if (interact != null)
        {
            if (interact.onCD)
                interactSpriteCDIndicator.SetActive(true);
            else
                interactSpriteCDIndicator.SetActive(false);
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
            interactSprite.gameObject.SetActive(true);
            interactSpriteGhost.gameObject.SetActive(true);
            interact = collision.GetComponent<Interact>();
            curInteract = 0;
            interactSprite.fillAmount = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Interactable")
            return;

        if(interact != null)
        {
            interactSprite.gameObject.SetActive(false);
            interactSpriteGhost.gameObject.SetActive(false);
            interactSpriteCDIndicator.SetActive(false);
            interact = null;
            curInteract = 0;
            interactSprite.fillAmount = 0;
        }
    }
}
