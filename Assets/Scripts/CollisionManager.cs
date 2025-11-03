using UnityEngine;

public class CollisionManager : MonoBehaviour {
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void PlayerCollision(GameObject player, Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            Debug.Log("Player collided with Ground.");
            gameManager.EndGame();
        }
        if (collision.gameObject.CompareTag("Obstacle")) {
            Debug.Log("Player collided with Obstacle.");
            gameManager.EndGame();
        }
    }
}
