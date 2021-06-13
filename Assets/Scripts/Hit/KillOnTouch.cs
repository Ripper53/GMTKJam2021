using UnityEngine;

public class KillOnTouch : MonoBehaviour {

    protected void OnCollisionEnter(Collision collision) {
        if (collision.collider.TryGetComponent(out Health health)) {
            health.Dead();
        } else if (collision.collider.TryGetComponent(out CoreBall ball)) {
            ball.ForceGrab();
        }
    }

}
