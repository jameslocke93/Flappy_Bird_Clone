using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private TextMeshProUGUI inGameScoreText;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;

    private GameManager gameManager;
    private TextMeshProUGUI scoreText;
    private GameManager.GameState previousState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        previousState = gameManager.GetCurrentState();
        scoreText = inGameScoreText;
        UpdateUI();
    }

    void Update() {
        if (previousState != gameManager.GetCurrentState()) {
            Debug.Log("Current Game State: " + gameManager.GetCurrentState().ToString());
            previousState = gameManager.GetCurrentState();
            UpdateUI();
        }

        scoreText.text = "Score: " + gameManager.GetScore().ToString();
    }

    private void UpdateUI() {
        switch (gameManager.GetCurrentState()) {
            case GameManager.GameState.MainMenu:
                ShowMainMenu();
                break;
            case GameManager.GameState.Playing:
                ShowInGameUI();
                break;
            case GameManager.GameState.GameOver:
                ShowGameOverUI();
                break;
        }
    }

    private void ShowMainMenu() {
        mainMenuUI.SetActive(true);
        gameOverUI.SetActive(false);
        inGameUI.SetActive(false);
    }

    private void ShowInGameUI() {
        mainMenuUI.SetActive(false);
        gameOverUI.SetActive(false);
        inGameUI.SetActive(true);

        scoreText = inGameScoreText;
    }

    private void ShowGameOverUI() {
        mainMenuUI.SetActive(false);
        gameOverUI.SetActive(true);
        inGameUI.SetActive(false);

        scoreText = gameOverScoreText;
    }
}
