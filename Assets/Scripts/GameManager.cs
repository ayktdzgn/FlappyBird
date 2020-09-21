using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Start,
        Playing,
        Menu,
        End
    }

    public GameState gameState;
    Bird bird;
    GrounManager grounManager;
    ObjectPool objectPool;
    MenuManager menuManager;

    [SerializeField]
    RectTransform pipeSpawnPoint;
    [SerializeField]
    float spawnRatio;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    GameObject menu;

    float startTime;
    public int score;

    private void Awake()
    {
        bird = FindObjectOfType<Bird>();
        grounManager = FindObjectOfType<GrounManager>();
        menuManager = menu.GetComponent<MenuManager>();
    }

    private void Start()
    {
        objectPool = ObjectPool.Instance;
        startTime = spawnRatio;

        bird.OnScoreIncrease += IncreaseScore;
        bird.OnHitNotifier += GameEnd;

        gameState = GameState.Start;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Start:
                ResetGame();
                menuManager.GameStart();
                CheckForStart();
                break;
            case GameState.Playing:
                menuManager.GamePlay();
                BirdFly();
                PipesSpawnInRatio();
                MovingGrounds();
                break;
            case GameState.End:
                menuManager.GameOver();
                break;
            case GameState.Menu:
                break;
            default:
                break;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    void GameEnd()
    {
        gameState = GameState.End;
        scoreText.text = "";
        if (score > PlayerInfo.GetScore())
        {
            PlayerInfo.SaveScore(score);
        }
    }

    void ResetGame()
    {
        score = 0;
        scoreText.text = score.ToString();
        bird.ResetPosition();
        bird.SetBirdTouchable();
        objectPool.DespawnObjects("Pipes");
    }

    void CheckForStart()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bird.Fly();
            bird.CheckForRotation();
            gameState = GameState.Playing;
        }
    }

    void PipesSpawnInRatio()
    {
        startTime += Time.deltaTime;
        if (startTime>spawnRatio)
        {
            startTime = 0f;
            PipesSpawn();
        }
    }

    void PipesSpawn()
    {
        Vector3 spawnPosition;
        spawnPosition = new Vector3(pipeSpawnPoint.position.x , Random.Range(-0.65f , 0.45f) , 0);
        objectPool.SpawnFromPool("Pipes", spawnPosition, Quaternion.identity);
    }

    void BirdFly()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bird.Fly();
        }
        bird.CheckForRotation();
    }

    void MovingGrounds()
    {
        grounManager.MovingGrounds();
    }
}
