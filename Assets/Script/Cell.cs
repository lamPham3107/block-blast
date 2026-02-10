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
    public void Normal()
    {
        gameObject.SetActive(true);
        spriteRenderer.color = Color.white;
        spriteRenderer.sprite = normalSprite;
    }
    public void Highlighted()
    {
        gameObject.SetActive(true);
        spriteRenderer.color = Color.white;
        spriteRenderer.sprite = highlightedSprite;
    }
    public void Hower()
    {
        gameObject.SetActive(true);
        spriteRenderer.color =  new Color(1.0f, 1.0f, 1.0f, 0.5f);
        spriteRenderer.sprite = normalSprite;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
