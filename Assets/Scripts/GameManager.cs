using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    private int score;
    private Text scoreText;
    private Animator textAnimator;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartScene()
    {
        score = PlayerPrefs.GetInt("Score", 0);
        RefindComponents();
    }

    public void AddScore()
    {
        score++;

        if (scoreText == null)
            Debug.LogError("Score text is null");
        scoreText.text = score.ToString();

        PlayerPrefs.SetInt("Score", score);

        if (textAnimator == null)
            Debug.LogError("ScoreText doesn't have an Animator component");
        textAnimator.SetTrigger("Add");
    }

    private void RefindComponents()
    {
        if (scoreText == null)
            scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();

        if (textAnimator == null)
            textAnimator = scoreText.gameObject.GetComponent<Animator>();

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
