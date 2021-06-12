using System.Collections.Generic;
using UnityEngine;

public class CorePlayer : MonoBehaviour {
	public Transform Transform;
	public Collider Collider;
	public CoreBall CoreBall;
	public CoreView CoreView;

	public float MoveSpeed;
	public float BallSpeed;
	public float DashSpeed;

	private CoreMovement coreMovement;

	private float chargeDelta;
	public float ChargeMax;
	public float ChargeRate;

	private bool dashing;
	private bool chargeCatch;

	private bool onGround;

	void Start() {
		coreMovement = GetComponent<CoreMovement>();
	}

	void Update () {

		if (Input.GetMouseButtonDown(0)) {
			if (CoreBall.IsDeployed()) {
				if (!dashing) {
					BeginDash();
					CoreBall.Fixate();
#if UNITY_EDITOR
					//DEBUG
					Debug.DrawRay(transform.position, (CoreBall.transform.position - transform.position).normalized, Color.blue, 100000);
					Debug.DrawRay(CoreBall.transform.position - Vector3.up / 2, Vector3.up, Color.red, 100000);
					Debug.DrawRay(CoreBall.transform.position - Vector3.right / 2, Vector3.right, Color.red, 100000);
					//Debug.Break ();
#endif
				}
			} else {
				chargeCatch = true;
			}
		} else if (Input.GetMouseButtonUp(0) && !CoreBall.IsDeployed()) {
			chargeCatch = false;
			CoreBall.Deploy(CoreView.GetSpawnPoint(), CoreView.GetSpawnDirection() * (chargeDelta / ChargeMax) * BallSpeed);
			chargeDelta = 0f;
		} else if (chargeCatch) {
			chargeDelta += Time.deltaTime * ChargeRate;
			if (chargeDelta > ChargeMax)
				chargeDelta = ChargeMax;
		}
	}

    private void FixedUpdate() {
		Vector3 delta = new Vector3(
			Input.GetAxis("Horizontal"),
			0f,
			Input.GetAxis("Vertical")
		);
		if (!dashing) {
			coreMovement.InputTick(delta * MoveSpeed);
		} else {
			DashToBall();
		}
	}

    private Vector3 dashOrigin;
	private float dashFill;
	private void DashToBall() {
		dashFill += DashSpeed * Time.fixedDeltaTime;
		if (dashFill >= 1f) {
			dashFill = 1f;
			EndDash();
        }
		Vector3 targetPos = CoreBall.GetPosition();
		RaycastHit[] hits = coreMovement.Rigidbody.SweepTestAll(CoreBall.GetPosition() - dashOrigin, Vector2.Distance(dashOrigin, CoreBall.GetPosition()), QueryTriggerInteraction.Ignore);
		if (hits.Length > 0) {
			RaycastHit hit = hits[hits.Length - 1];
			if (hit.collider.ClosestPoint(targetPos) == targetPos) {
				Vector3 point = hit.point;
				Vector3 diff = Collider.ClosestPoint(point) - Collider.transform.position;
				targetPos = point + diff;
			}
		}
		Transform.position = Vector3.Lerp(dashOrigin, targetPos, dashFill);
	}

	public void Returned() {
		CoreView.EndOverride();
		EndDash();
		dashing = false;
	}

	private void BeginDash() {
		dashing = true;
		coreMovement.Rigidbody.detectCollisions = false;
		coreMovement.Rigidbody.isKinematic = true;
		coreMovement.Rigidbody.velocity = Vector2.zero;
		coreMovement.enabled = false;
		dashFill = 0f;
		dashOrigin = Transform.position;
    }
	private void EndDash() {
		CoreBall.Grab();
		coreMovement.Rigidbody.detectCollisions = true;
		coreMovement.Rigidbody.isKinematic = false;
		coreMovement.enabled = true;
		dashing = false;
	}

}
