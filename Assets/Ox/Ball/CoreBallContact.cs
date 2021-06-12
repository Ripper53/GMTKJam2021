using UnityEngine;

public class CoreBallContact : MonoBehaviour {
    public Transform Transform;
    public float VerticalDistance, HorizontalDistance;
    public LayerMask DetectionLayerMask;

    public Vector3 Contact { get; private set; }
    public bool InContact { get; private set; }

    protected void FixedUpdate() {
        InContact =
            Raycast(Vector3.down, VerticalDistance) ||
            Raycast(Vector3.up, VerticalDistance) ||
            Raycast(Vector3.forward, HorizontalDistance) ||
            Raycast(Vector3.back, HorizontalDistance) ||
            Raycast(Vector3.right, HorizontalDistance) ||
            Raycast(Vector3.left, HorizontalDistance);
    }

    private bool Raycast(Vector3 direction, float distance) {
        if (Physics.Raycast(Transform.position, direction, out RaycastHit hit, distance, DetectionLayerMask, QueryTriggerInteraction.Ignore)) {
            Contact = Transform.position + (direction * (hit.distance - VerticalDistance));
            return true;
        }
        return false;
    }

}
