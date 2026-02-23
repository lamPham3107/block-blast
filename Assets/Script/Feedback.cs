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


    public void ShowFeedbackRandom()
    {
        int index = Random.Range(0, FeedbackSprites.Length);
        ShowFeedbackSprite(FeedbackSprites[index]);
    }

    private void ShowFeedbackSprite(Sprite sprite)
    {
        if (Img_feedback == null || sprite == null) return;

        CancelInvoke(nameof(HideFeedback));
        Img_feedback.sprite = sprite;
        Img_feedback.gameObject.SetActive(true);

        Invoke(nameof(HideFeedback), displayDuration);
    }

    private void HideFeedback()
    {
        if (Img_feedback != null)
        {
            Img_feedback.gameObject.SetActive(false);
        }
    }

}