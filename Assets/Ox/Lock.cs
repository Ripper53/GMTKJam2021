using UnityEngine;

public class Lock : MonoBehaviour {
    public Transform Target;

    protected void LateUpdate() {
        transform.position = Target.position;
    }

}
