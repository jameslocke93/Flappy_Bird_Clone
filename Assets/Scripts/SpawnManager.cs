using System;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] private Vector3 playerSpawnLocation;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private double maxObstacleAngle;
    [SerializeField] private float obstacleGap;
    [SerializeField] private float obstacleMinYPosition;
    [SerializeField] private float obstacleMaxYPosition;
    [SerializeField] private float obstacleSpawnInterval;
    [SerializeField] private int obstacleSpawnXPosition;
    [SerializeField] private int obstacleSpawnDelay;

    private GameManager gameManager;
    private GameManager.GameState previousState;
    private GameObject player;
    private double angleRad;
    private float obstacleSpawnTimer;
    private float obstacleSpawnDelayTimer;
    private float previousY;
    private float obstacleMoveSpeed;
    private float maxYPos;
    private float minYPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        obstacleMoveSpeed = obstaclePrefab.GetComponent<MoveLeft>().GetMoveSpeed();
        previousState = gameManager.GetCurrentState();

        // Convert degrees -> radians for Math.Tan.
        angleRad = maxObstacleAngle * Math.PI / 180.0;

        // Init each game loop
        obstacleSpawnTimer = 0f;
        obstacleSpawnDelayTimer = 0f;
        previousY = 0f;
        maxYPos = obstacleMaxYPosition;
        minYPos = obstacleMinYPosition;

        if (maxObstacleAngle > 89) {
            Debug.LogWarning("Max obstacle angle too high, setting to 89 degrees");
            maxObstacleAngle = 89;
        } else if (maxObstacleAngle < 1) {
            Debug.LogWarning("Max obstacle angle too low, setting to 1 degree");
            maxObstacleAngle = 1;
        }
    }

    void Update() {
        switch(gameManager.GetCurrentState()) {
            case GameManager.GameState.Playing:
                // TODO: Need to check for gameManager difficulty settings to adjust spawn rate/speed
                if (previousState == GameManager.GameState.MainMenu) {
                    SpawnPlayer();
                }
                if (obstacleSpawnDelayTimer >= obstacleSpawnDelay) {
                    StartSpawningObstacles();
                } else {
                    obstacleSpawnDelayTimer += Time.deltaTime;
                }
                break;
            case GameManager.GameState.MainMenu:
                if (previousState == GameManager.GameState.GameOver) {
                    // Reset everything
                    // Not sure if this is the best place to do this
                    DespawnPlayer();
                    DespawnObstacles();

                    obstacleSpawnTimer = 0f;
                    obstacleSpawnDelayTimer = 0f;
                    previousY = 0f;
                    maxYPos = obstacleMaxYPosition;
                    minYPos = obstacleMinYPosition;
                }
                break;
        }
        previousState = gameManager.GetCurrentState();
    }



    private void SpawnPlayer() {
        player = Instantiate(playerPrefab, playerSpawnLocation, playerPrefab.transform.rotation);
    }

    private float SpawnObstacle(float minYPos, float maxYPos, float xPos, float gap) {
        // Need to spawn two obstacles with a gap in between
        // Spawn two obstacles based on random Y position - gap
        float obstacleYPosition = UnityEngine.Random.Range(minYPos, maxYPos);
        Vector3 upperObstaclePosition = new (xPos, obstacleYPosition, 0);
        Vector3 lowerObstaclePosition = new (xPos, obstacleYPosition - gap, 0);
        Instantiate(obstaclePrefab, upperObstaclePosition, obstaclePrefab.transform.rotation);
        Instantiate(obstaclePrefab, lowerObstaclePosition, obstaclePrefab.transform.rotation);

        return obstacleYPosition;
    }

    private void StartSpawningObstacles() {
        obstacleSpawnTimer += Time.deltaTime;

        if (obstacleSpawnTimer >= obstacleSpawnInterval) {
            previousY = SpawnObstacle(minYPos, maxYPos, obstacleSpawnXPosition, obstacleGap);

            // Generate new Y range for next obstacle spawn
            (maxYPos, minYPos) = GenerateObstacleYRange(previousY);
            obstacleSpawnTimer = 0f;
        }
    }

    private (float, float) GenerateObstacleYRange(float previousY) {
        // Coordinates can be local, no need for global
        // IDEA: Need to store old obstacle positions to calculate angle between them if using dynamic spawn intervals
        float previousX = obstacleMoveSpeed * obstacleSpawnInterval;

        double height = previousX * Math.Tan(angleRad);

        float newMinYPos = previousY - (float)height;
        float newMaxYPos = previousY + (float)height;

        if (newMinYPos < obstacleMinYPosition) {
            newMinYPos = obstacleMinYPosition;
        } else if (newMinYPos > obstacleMaxYPosition) {
            newMinYPos = obstacleMaxYPosition;
        }
        if (newMaxYPos > obstacleMaxYPosition) {
            newMaxYPos = obstacleMaxYPosition;
        } else if (newMaxYPos < obstacleMinYPosition) {
            newMaxYPos = obstacleMinYPosition;
        }
        return (newMinYPos, newMaxYPos);
    }

    private void DespawnPlayer() {
        Destroy(player);
    }

    private void DespawnObstacles() {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles) {
            Destroy(obstacle);
        }
    }
}
