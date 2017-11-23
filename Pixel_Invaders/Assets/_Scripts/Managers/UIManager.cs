using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Camera mainCamera;
    public Text scoreLabel;
    public Text highScoreLabel;
    public Slider staminaSlider;
    public Slider progressSlider;
    public Text levelLabel;
    public Text gameOverLabel;
    public Button retryButton;
    public Button titleButton;

    private string scoreText = "Score : ";
    private string highscoreText = "Highscore: ";
    private string levelText = "Level ";

	// Use this for initialization
	void Start () {
        scoreLabel.text = scoreText + 0;
        staminaSlider.value = 1;
        gameOverLabel.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        titleButton.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateScore(int score)
    {
        scoreLabel.text = scoreText + score;
    }

    public void UpdateHighScore(int highscore)
    {
        highScoreLabel.text = highscoreText + highscore;
    }

    public void SetStaminaSlider(int stamina)
    {
        float sliderPercent = (float)stamina / 100;
        staminaSlider.value = sliderPercent;
    }

    public void SetProgressSlider(int progress)
    {
        float sliderPercent = (float)progress / 100;
        progressSlider.value = sliderPercent;
    }

    public void ShowGameOver()
    {
        levelLabel.gameObject.SetActive(false);
        gameOverLabel.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        titleButton.gameObject.SetActive(true);
    }

    public void ShowLevelLabel(int i)
    {
        levelLabel.gameObject.SetActive(true);
        levelLabel.text = levelText + i;
        
    }

    public void HideLevelLabel()
    {
        levelLabel.gameObject.SetActive(false);
    }

    public void ShowAddScore(Vector3 objectPos,int score)
    {
        mainCamera.WorldToScreenPoint(objectPos);
        //Text scoreText = new Text();
       // "+" + score

    }
    
}
