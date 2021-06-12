using UnityEngine;

public class CoreMovement : MonoBehaviour {
	public Rigidbody Rigidbody;
	public float Drag;

	private Vector3 tickDelta;
	private Vector3 speedDelta;

	public void InputTick(Vector3 a1) {
		tickDelta += Rigidbody.rotation * a1;
	}

	public Vector3 GetDeltaSpeed() {
		return speedDelta;
	}

	private void FixedUpdate() {
		Vector3 vel = Rigidbody.velocity;
		if (speedDelta.x != 0f)
			vel.x = speedDelta.x;
		if (speedDelta.y != 0f)
			vel.y = speedDelta.y;
		if (speedDelta.z != 0f)
			vel.z = speedDelta.z;
		Rigidbody.velocity = vel;

		speedDelta += tickDelta;

		speedDelta -= speedDelta * Drag * Time.fixedDeltaTime;

		tickDelta = Vector3.zero;
	}

	public void Reinitialize() {
		speedDelta = Vector3.zero;
		Rigidbody.velocity = Vector3.zero;
	}

}
