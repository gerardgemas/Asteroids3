using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject gameOverMenu;
    public GameObject playingMenu;
    public GameManager gameManager;
    public GameObject live1;
    public GameObject live2;
    public GameObject live3;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    private const string HighScoreKey = "HighScore";
    public TextMeshProUGUI highScoreText2;
    // Start is called before the first frame update
    void Start()
    {
        int highScore = PlayerPrefs.GetInt(HighScoreKey, 0); 
        highScoreText2.text = "High Score: " + highScore.ToString(); 
        highScoreText.text = "High Score: " + highScore.ToString(); 
    }
    void Update()
    {
        updateLivesHUD();
        updateScore();
    }

    public void updateScore()
    {
        scoreText.text = "Score: " + gameManager.score.ToString();
    }

    public void pressPlay()
    {
        gameManager.changeGameState();
        gameStatePlay();
    }
    public void gameStatePlay()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        playingMenu.SetActive(true); 
    }
    public void gameStateOver()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        playingMenu.SetActive(false); 
    }
    public void updateLivesHUD()
    {
        switch(gameManager.lives)
        {
            case 3: 
            live1.SetActive(true);
            live2.SetActive(true);
            live3.SetActive(true);
            break; 
            case 2: 
            live1.SetActive(true);
            live2.SetActive(true);
            live3.SetActive(false);
            break; 
            case 1: 
            live1.SetActive(true);
            live2.SetActive(false);
            live3.SetActive(false);
            break; 
            case 0: 
            live1.SetActive(false);
            live2.SetActive(false);
            live3.SetActive(false);
            break; 
        }
    }
    public void UpdateHighScore()
    {

        int savedHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        if (gameManager.score > savedHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, gameManager.score);
            PlayerPrefs.Save(); 
            highScoreText.text = "High Score: " + gameManager.score.ToString(); 
        }
    }
    public void restartHUD()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        playingMenu.SetActive(true); 
    }

}
