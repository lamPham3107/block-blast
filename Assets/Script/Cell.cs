using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] public Sprite[] colorSprites;
    [SerializeField] private Sprite highlightedSprite;
    private Sprite currentSprite;
    private int currentColorIndex;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (colorSprites != null && colorSprites.Length > 0)
        {
            currentSprite = colorSprites[0];
        }
    }

    public void Normal()
    {
        gameObject.SetActive(true);
        spriteRenderer.color = Color.white;
        spriteRenderer.sprite = currentSprite;
    }
    public void Highlighted()
    {
        gameObject.SetActive(true);
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        Debug.Log("Highlighted");
    }
    public void UnHighlighted()
    {
        gameObject.SetActive(true);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

    }
    public void Hower()
    {
        gameObject.SetActive(true);
        spriteRenderer.color =  new Color(1.0f, 1.0f, 1.0f, 0.5f);
        spriteRenderer.sprite = currentSprite;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetColor(int colorIndex)
    {
        var Sprite = colorSprites[colorIndex];
        currentSprite = Sprite;
    }


}
