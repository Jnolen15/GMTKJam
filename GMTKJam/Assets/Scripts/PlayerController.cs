using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    // ================= Variables =================
    [SerializeField] private float moveSpeed;
    [SerializeField] private float curInteract;

    // ================= Refrences =================
    [SerializeField] private Image interactSprite;
    [SerializeField] private Image interactSpriteGhost;
    [SerializeField] private GameObject interactSpriteCDIndicator;
    [SerializeField] private TextMeshProUGUI interactTitle;
    [SerializeField] private TextMeshProUGUI interactDescription;
    [SerializeField] private GameObject textBG;
    [SerializeField] private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Vector2 movement;
    private GameplayManager gManager;
    [SerializeField] private Interact interact;
    [SerializeField] private bool isAnimating;

    // ================= Events =================
    public delegate void PlayerAction();
    public static event PlayerAction OnSabotage;
    public static event PlayerAction OnActNormal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameplayManager>();
    }

    void Update()
    {
        // Calulate movement
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement.Normalize();

        // Animation
        if (Mathf.Abs(movement.x) > 0 || Mathf.Abs(movement.y) > 0)
        {
            StartAnimate();
            isAnimating = true;
        }
        else
        {
            EndAnimate();
            isAnimating = false;
        }

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
                {
                    OnSabotage();
                    gManager.AddMultiplier(1);
                    gManager.AddScore(interact.ScoreVal);
                }
                else
                {
                    OnActNormal();
                    gManager.AddScore(interact.ScoreVal);
                }

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

    private void StartAnimate()
    {
        if (isAnimating)
            return;

        // Animate
        sprite.transform.rotation = Quaternion.Euler(0, 0, -5);
        sprite.transform.localPosition = Vector3.zero;
        sprite.transform.DOLocalJump(sprite.transform.localPosition, 0.2f, 1, 0.6f).SetLoops(-1, LoopType.Restart).SetEase(Ease.InOutSine);
        sprite.transform.DORotate(new Vector3(0, 0, 10), 0.6f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    private void EndAnimate()
    {
        sprite.transform.DOKill();
        sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            interact = collision.GetComponent<Interact>();
            interactSprite.gameObject.SetActive(true);
            interactSpriteGhost.gameObject.SetActive(true);
            interactTitle.gameObject.SetActive(true);
            interactDescription.gameObject.SetActive(true);
            textBG.gameObject.SetActive(true);

            if (interact.isSabotage)
            {
                interactDescription.text = interact.description;
                interactTitle.text = "Sabotage";
                interactTitle.color = Color.red;
                interactDescription.color = Color.red;
            } else {
                interactDescription.text = interact.description;
                interactTitle.text = "Act Natural";
                interactTitle.color = Color.green;
                interactDescription.color = Color.green;
            }

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
            interactTitle.gameObject.SetActive(false);
            interactDescription.gameObject.SetActive(false);
            textBG.gameObject.SetActive(false);
            interact = null;
            curInteract = 0;
            interactSprite.fillAmount = 0;
        }
    }
}
