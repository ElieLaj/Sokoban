using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState gameState;

    private Vector2 playerPos;

    public TMP_Text winText;
    public Button replayButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        winText.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(false);
        ChangeState(gameState);
    }

    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.R))
        {
            ChangeState(GameState.RELOAD);
        }
    }

    public void ChangeState(GameState newState)
    {
        gameState = newState;

        Debug.Log($"Game State Changed to {gameState}");

        switch (gameState)
        {
            case GameState.PLAYING:
                break;
            case GameState.PAUSED:
                break;
            case GameState.GAME_OVER:
                break;
            case GameState.WIN:
                winText.gameObject.SetActive(true);
                replayButton.gameObject.SetActive(true);
                break;
            case GameState.SPAWNTILES:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SPAWNPLAYER:
                EntityManager.Instance.SpawnEntities();
                break;
            case GameState.RELOAD:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;

        }
    }
}
public enum GameState
{
    PLAYING,
    PAUSED,
    GAME_OVER,
    WIN,
    SPAWNTILES,
    SPAWNPLAYER,
    RELOAD
}
