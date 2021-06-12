using UnityEngine;

public class CoreMovement : MonoBehaviour {
	public Rigidbody Rigidbody;

	private Vector3 speedDelta = Vector2.zero;

	public void AddVelocity(Vector3 vel) {
		speedDelta += Rigidbody.rotation * vel;
	}

	protected void FixedUpdate() {
		Vector3 vel = Rigidbody.velocity;
		if (speedDelta.x != 0f)
			vel.x = speedDelta.x;
		if (speedDelta.y != 0f)
			vel.y = speedDelta.y;
		if (speedDelta.z != 0f)
			vel.z = speedDelta.z;
		speedDelta = Vector3.zero;
		Rigidbody.velocity = vel;
	}

	public void Reinitialize() {
		speedDelta = Vector3.zero;
		Rigidbody.velocity = Vector3.zero;
	}

}
