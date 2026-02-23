using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private Image end_game_image;
    [SerializeField] private Image end_game_popup;
    [SerializeField] private TextMeshProUGUI txt_score_end_game;
    [SerializeField] private TextMeshProUGUI txt_high_score;
    private int currentScore;

    public static GameController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // ✅ Sửa: Destroy gameObject, không phải this
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        end_game_popup.gameObject.SetActive(false);
        end_game_image.gameObject.SetActive(false);
        Feedback.Instance.Img_feedback.gameObject.SetActive(false);
        currentScore = 0;
        UpdateScore();
    }

    public void AddScore()
    {
        currentScore += 8;
        UpdateScore();
    }
    public void UpdateScore()
    {
        score.text = currentScore.ToString();
    }

    public void Lose()
    {
        var time = 1.5f;
        end_game_image.gameObject.SetActive(true);
        SFX.Instance.PlayGameOverSFX();
        SetScore();
        while (time > 0.0f)
        {
            time -= Time.deltaTime;

        }
        if (time <= 0.0f)
        {
            end_game_popup.gameObject.SetActive(true);

        }
    }

    public void Restart()
    {
        currentScore = 0;
        UpdateScore();
        end_game_image.gameObject.SetActive(false);
        end_game_popup.gameObject.SetActive(false);
        Feedback.Instance.Img_feedback.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
    private void SetScore()
    {
        txt_score_end_game.text = currentScore.ToString();
        var highScore = PlayerPrefs.GetInt("high_score", 0);
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("high_score", currentScore);
            PlayerPrefs.Save();
            highScore = currentScore;

        }
        txt_high_score.text = highScore.ToString();

    }
}
