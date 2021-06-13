using UnityEngine;

public class DangerZone : MonoBehaviour {
    public Transform Origin;
    public float Radius;
    public LayerMask DetectionLayerMask;
    public int Damage = 1;

    protected void FixedUpdate() {
        foreach (Collider collider in Physics.OverlapSphere(Origin.position, Radius, DetectionLayerMask, QueryTriggerInteraction.Ignore)) {
            if (collider.TryGetComponent(out Health health))
                health.Damage(Damage);
        }
    }

#if UNITY_EDITOR
    protected void OnDrawGizmosSelected() {
        if (!Origin) return;
        Gizmos.color = Color.red;
        Gizmos.matrix = Origin.localToWorldMatrix;
        Gizmos.DrawWireSphere(Vector3.zero, Radius);
    }
#endif

}
