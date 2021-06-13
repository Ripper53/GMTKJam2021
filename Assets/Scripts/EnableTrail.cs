using UnityEngine;

public class EnableTrail : MonoBehaviour {
    public Transform Transform, Target;
    public TrailRenderer TrailRenderer;

    protected void Awake() {
        Transform.position = Target.position;
        TrailRenderer.enabled = true;
    }

}
