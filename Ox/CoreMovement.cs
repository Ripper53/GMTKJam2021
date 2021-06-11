using UnityEngine;

public class CoreMovement : MonoBehaviour {
	public	Vector3		speed;
	public  float	    drag;

	private Vector3		tickDelta;
	private Vector3Int	tickCount;

	private Vector3     speedDelta;
	private Quaternion  rotDelta;

	private Rigidbody   target;

	public void InputTick( Vector3 a1 ) {
		tickDelta += a1;
		tickCount += Vector3Int.one;
	}

	private void Start () {
		target = GetComponent<Rigidbody> ();
	}

	private void FixedUpdate () {
		if( tickCount.x != 0 ) tickDelta.x /= tickCount.x;
		if( tickCount.y != 0 ) tickDelta.y /= tickCount.y;
		if( tickCount.z != 0 ) tickDelta.z /= tickCount.z;

		target.MovePosition ( transform.position + speedDelta * Time.fixedDeltaTime );

		speedDelta = Quaternion.RotateTowards ( rotDelta, target.rotation, 360 ) * speedDelta;

		speedDelta += Vector3.Scale( tickDelta, speed );

		speedDelta -= speedDelta * drag * Time.fixedDeltaTime;

		rotDelta = target.rotation;
		tickCount = Vector3Int.zero;
		tickDelta = Vector3.zero;
	}
}