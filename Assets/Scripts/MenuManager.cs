using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour

{
    GameManager gameManager;

    [SerializeField]
    GameObject gameStartMenu;
    [SerializeField]
    GameObject gameOverMenu;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text bestScoreText;
    [SerializeField]
    Image medal;

    public Sprite[] medalImages;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void GameStart()
    {
        gameStartMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        Time.timeScale = 0;
    }

    public void GamePlay()
    {
        gameStartMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        gameManager.gameState = GameManager.GameState.Menu;
        gameOverMenu.SetActive(true);
        scoreText.text = gameManager.score.ToString();
        bestScoreText.text = PlayerInfo.GetScore().ToString();
        if (PlayerInfo.GetScore()>100)
        {
            medal.sprite = medalImages[2];
        }else if(PlayerInfo.GetScore() > 50)
        {
            medal.sprite = medalImages[2];
        }

        gameOverMenu.GetComponentInChildren<Button>().onClick.AddListener(() => {
            gameOverMenu.SetActive(false);
            gameManager.gameState = GameManager.GameState.Start;
        });
    }
}
