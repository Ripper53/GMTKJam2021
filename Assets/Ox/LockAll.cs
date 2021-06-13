using UnityEngine;

public class LockAll : MonoBehaviour {
    public Transform Transform, Target;

    protected void LateUpdate() {
        Transform.position = Target.position;
        Transform.rotation = Target.rotation;
    }

}
