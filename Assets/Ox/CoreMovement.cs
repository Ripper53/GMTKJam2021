using UnityEngine;

public class CoreMovement : MonoBehaviour {
	public float Speed;
	public float Drag;

	private Vector3 tickDelta;
	private int tickCount;

	private Vector3 speedDelta;

	private Rigidbody target;

	public void InputTick( Vector3 a1 ) {
		tickDelta += target.rotation * a1.normalized;
		tickCount++;
	}

	public void Override ( Vector3 a1 ) {
		speedDelta = Vector3.zero;
		Speed = 1f;
		Drag = 0;
		tickDelta = a1;
		tickCount = 1;
		target.useGravity = false;
	}

	public void EndOverride () {
		speedDelta /= 3;
		target.useGravity = true;
	}

	public Vector3 GetDeltaSpeed () {
		return speedDelta;
	}

	private void Start () {
		target = GetComponent<Rigidbody> ();
	}

	private void FixedUpdate () {
		if ( tickCount != 0 ) {
			tickDelta /= tickCount;
		}

		target.MovePosition ( transform.position + speedDelta * Time.fixedDeltaTime );

		speedDelta += tickDelta * Speed;

		speedDelta -= speedDelta * Drag * Time.fixedDeltaTime;

		tickCount = 0;
		tickDelta = Vector3.zero;
	}
}