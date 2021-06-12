using UnityEngine;

public class GroundCheck : MonoBehaviour {
    public Transform Transform;
    public float Radius;
    public LayerMask DetectionLayerMask;

    public bool Evaluate() {
        return Physics.CheckSphere(Transform.position, Radius, DetectionLayerMask, QueryTriggerInteraction.Ignore);
    }

#if UNITY_EDITOR
    protected void OnDrawGizmosSelected() {
        if (!Transform) return;
        Gizmos.color = Color.green;
        Gizmos.matrix = Transform.localToWorldMatrix;
        Gizmos.DrawWireSphere(Vector3.zero, Radius);
    }
#endif

}
