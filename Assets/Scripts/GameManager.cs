using UnityEngine;

public class GameManager : MonoBehaviour {
    public enum GameState { MainMenu, Playing, GameOver }
    private GameState currentState;
    public enum DifficultyState { Normal, Scaling}
    private DifficultyState currentDifficulty;

    private int score;
    private float scoreInterval;
    private float scoreTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        scoreTimer = 0.0f;
        scoreInterval = 1.0f;

        currentState = GameState.MainMenu;
    }

    // Update is called once per frame
    void Update() {
        if (currentState == GameState.Playing) {
            UpdateScore();
        }
    }
    private void UpdateScore() {
        scoreTimer += Time.deltaTime;
        if (scoreTimer >= scoreInterval) {
            score += 1;
            scoreTimer = 0.0f;
        }
    }

    public void StartGame(int difficultyLevel) {
        Debug.Log("Game Started!");
        // Add game start logic here
        score = 0;
        scoreTimer = 0.0f;

        currentDifficulty = (difficultyLevel == 0) ? DifficultyState.Normal : DifficultyState.Scaling;

        currentState = GameState.Playing;
    }

    public void EndGame() {
        Debug.Log("Game Ended!");
        // Add game end logic here
        currentState = GameState.GameOver;
    }

    public void BackToMainMenu() {
        currentState = GameState.MainMenu;
    }

    public int GetScore() { return score; }
    public DifficultyState GetDifficultyLevel() { return currentDifficulty; }
    public GameState GetCurrentState() { return currentState; }
    public int GetCurrentScore() { return score; }
}
