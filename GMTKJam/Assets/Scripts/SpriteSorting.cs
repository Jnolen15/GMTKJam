using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorting : MonoBehaviour
{
    // ================= Refrences =================
    [SerializeField] private SpriteRenderer sprite;

    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        sprite.sortingOrder = (int)(transform.position.y * -10);
    }
}
