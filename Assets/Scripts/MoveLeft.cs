using UnityEngine;

public class MoveLeft : MonoBehaviour {
    [SerializeField] private int leftSpeed;

    private GameManager gameManager;

    void Start() {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update() {
        if (gameManager.GetCurrentState() != GameManager.GameState.Playing) {
            return;
        }
        transform.Translate(Vector3.left * Time.deltaTime * leftSpeed);
    }

    public float GetMoveSpeed() { return leftSpeed; }
}
