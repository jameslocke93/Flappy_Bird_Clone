using UnityEngine;

public class OutOfBounds : MonoBehaviour {
    [SerializeField] private int lowerXBound;

    void Update() {
        // TODO: Can be moved slow update to improve performance
        if (gameObject.CompareTag("Obstacle") && transform.position.x < lowerXBound) {
            Destroy(gameObject);
        }
    }
}
