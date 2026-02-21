using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] colorSprites;
    [SerializeField] private Sprite highlightedSprite;
    private Sprite currentSprite;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSprite = SetRamdomColor();
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
        spriteRenderer.color = Color.white;
        spriteRenderer.sprite = highlightedSprite;
        Debug.Log("Highlighted");
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

    private Sprite SetRamdomColor()
    {
        var index = Random.Range(0, colorSprites.Length);
        var Sprite = colorSprites[index];
        return Sprite;
    }

}
