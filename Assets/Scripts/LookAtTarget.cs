using UnityEngine;

public class LookAtTarget : MonoBehaviour {
    public Transform Transform;
    public CorePlayer CorePlayer;

    protected void LateUpdate() {
        Transform.LookAt(CorePlayer.GetPosition());
    }

}
