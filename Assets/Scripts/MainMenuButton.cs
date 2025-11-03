using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour {
    [SerializeField] private int difficultyLevel;

    private GameManager gameManager;
    private Button button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(MainMenuStart);
    }

    public void MainMenuStart() {
        gameManager.StartGame(difficultyLevel);
    }
}
