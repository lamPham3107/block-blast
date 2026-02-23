using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feedback : MonoBehaviour
{
    [SerializeField] public Image Img_feedback;
    [SerializeField] private Sprite[] FeedbackSprites;
    [SerializeField] private float displayDuration = 1f;

    public static Feedback Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (Img_feedback != null)
        {
            Img_feedback.gameObject.SetActive(false);
        }
    }

    public void ShowFeedback(int linesClearedCount)
    {
        int index = GetFeedbackIndex(linesClearedCount);
        if (index >= 0 && index < FeedbackSprites.Length)
        {
            ShowFeedbackSprite(FeedbackSprites[index]);
        }
    }

    public void ShowFeedbackRandom()
    {
        int index = Random.Range(0, FeedbackSprites.Length);
        ShowFeedbackSprite(FeedbackSprites[index]);
    }

    private void ShowFeedbackSprite(Sprite sprite)
    {
        if (Img_feedback == null || sprite == null) return;

        // Cancel invoke cũ nếu có
        CancelInvoke(nameof(HideFeedback));

        // Show
        Img_feedback.sprite = sprite;
        Img_feedback.gameObject.SetActive(true);

        // ✅ Tự động ẩn sau displayDuration giây
        Invoke(nameof(HideFeedback), displayDuration);
    }

    private void HideFeedback()
    {
        if (Img_feedback != null)
        {
            Img_feedback.gameObject.SetActive(false);
        }
    }

    private int GetFeedbackIndex(int linesClearedCount)
    {
        switch (linesClearedCount)
        {
            case 1: return 0; // GOOD
            case 2: return 1; // GREAT
            case 3: return 2; // PERFECT
            case 4: return 3; // NICE
            default: return linesClearedCount > 4 ? 4 : 0;
        }
    }
}