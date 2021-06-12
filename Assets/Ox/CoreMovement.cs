using UnityEngine;

public class CoreMovement : MonoBehaviour {
	public	Vector3		speed;
	public  float	    drag;

	private Vector3		tickDelta;
	private int			tickCount;

	private Vector3     speedDelta;

	private Rigidbody   target;

	public void InputTick( Vector3 a1 ) {
		tickDelta += target.rotation * a1;
		tickCount++;
	}

	public void Override ( Vector3 a1 ) {
		speedDelta = Vector3.zero;
		speed = Vector3Int.one;
		drag = 0;
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
			tickDelta.x /= tickCount;
			tickDelta.y /= tickCount;
			tickDelta.z /= tickCount;
		}

		target.MovePosition ( transform.position + speedDelta * Time.fixedDeltaTime );

		speedDelta += Vector3.Scale( tickDelta, speed );

		speedDelta -= speedDelta * drag * Time.fixedDeltaTime;

		tickCount = 0;
		tickDelta = Vector3.zero;
	}
}