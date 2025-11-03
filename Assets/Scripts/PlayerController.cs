using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float upForce;
    [SerializeField] private int maxHeight = 6;

    private Rigidbody playerRigidbody;
    private CollisionManager collisionManager;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        playerRigidbody = GetComponent<Rigidbody>();
        collisionManager = GameObject.Find("Collision Manager").GetComponent<CollisionManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Add upforce at start
        ApplyUpwardForce(upForce / 2);
    }

    void Update() {
        if (gameManager.GetCurrentState() != GameManager.GameState.Playing) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            ApplyUpwardForce(upForce);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            gameManager.EndGame();
        }
    }

    private void ApplyUpwardForce(float force) {
        if (transform.position.y >= maxHeight) {
            Debug.Log("Player is at maximum height, cannot apply more upward force.");
            return;
        }
        playerRigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
        // Debug.Log("Player applied upward force at: " + upForce);
    }

    private void OnCollisionEnter(Collision collision) {
        collisionManager.PlayerCollision(gameObject, collision);
    }
}
