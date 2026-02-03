using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite highlightedSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Normal()
    {
        gameObject.SetActive(true);
        spriteRenderer.color = Color.white;
        spriteRenderer.sprite = normalSprite;
    }
    private void Highlighted()
    {
        gameObject.SetActive(true);
        spriteRenderer.color = Color.white;
        spriteRenderer.sprite = highlightedSprite;
    }
    private void Hower()
    {
        gameObject.SetActive(true);
        spriteRenderer.color =  new Color(1.0f, 1.0f, 1.0f, 0.5f);
        spriteRenderer.sprite = normalSprite;
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
