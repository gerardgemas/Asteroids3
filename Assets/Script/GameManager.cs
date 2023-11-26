using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public int score = 0;
    public bool play = false;
    public MenuManager menuManager;
    private bool alreadyOver = false;
    public Player player;
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject); // Ensures only one GameManager exists
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkGameOver();
    }
    public void changeGameState()
    {
        play = !play;
    }
    public void checkGameOver()
    {
        if (lives == 0 && !alreadyOver)
        {
            changeGameState();
            menuManager.gameStateOver();
            menuManager.UpdateHighScore();
            alreadyOver = true;
        }
    }
    public bool getPlay()
    {
        return play;
    }
    public void SubtractLife()
    {
        lives--;
        if (lives <= 0)
        {
            lives = 0;
            menuManager.gameStateOver();
        }
    }
    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void restartGame()
    {
        menuManager.restartHUD();
        changeGameState();
        lives = 3;
        score = 0;
        player.returnOriginalPosition();
        alreadyOver = false;
    }
    public void IncreaseScore(){
        score = score +1;
    }
}
