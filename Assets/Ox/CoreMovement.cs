using UnityEngine;

public class CoreMovement : MonoBehaviour {
	public Rigidbody Rigidbody;
	public float Drag;

	private Vector3 tickDelta;
	private int tickCount;

	private Vector3 speedDelta;

	public void InputTick( Vector3 a1 ) {
		tickDelta += Rigidbody.rotation * a1;
		tickCount++;
	}

	public Vector3 GetDeltaSpeed () {
		return speedDelta;
	}

	private void FixedUpdate() {
		if (tickCount != 0) {
			tickDelta /= tickCount;
		}

		Rigidbody.MovePosition(transform.position + speedDelta * Time.fixedDeltaTime);

		speedDelta += tickDelta;

		speedDelta -= speedDelta * Drag * Time.fixedDeltaTime;

		tickCount = 0;
		tickDelta = Vector3.zero;
	}
}