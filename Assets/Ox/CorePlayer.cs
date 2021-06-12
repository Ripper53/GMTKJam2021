using System.Collections.Generic;
using UnityEngine;

public class CorePlayer : MonoBehaviour {
	public Transform Transform;
	public CapsuleCollider Collider;
	public LayerMask DashLayerMask;
	public CoreBall CoreBall;
	public CoreBallContact CoreBallContact;
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

	void Start() {
		coreMovement = GetComponent<CoreMovement>();
	}

	void Update () {

		if (Input.GetMouseButtonDown(0)) {
			if (CoreBall.IsDeployed()) {
				if (!dashing) {
					BeginDash();
					CoreBall.Fixate();
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
		RaycastEnd(ref targetPos);
		Transform.position = Vector3.Lerp(dashOrigin, targetPos, dashFill);
	}

	private void RaycastEnd(ref Vector3 targetPos) {

		if (CoreBallContact.InContact)
			targetPos = CoreBallContact.Contact;

		Vector3 h = new Vector3(0f, Collider.height / 2f, 0f);
		/*Collider[] colliders = Physics.OverlapCapsule(
			targetPos + h,
			targetPos - h,
			Collider.radius,
			DashLayerMask,
			QueryTriggerInteraction.Ignore
		);*/
		/*Collider[] colliders = Physics.OverlapSphere(
			targetPos, 0.1f, DashLayerMask, QueryTriggerInteraction.Ignore
		);
		if (colliders.Length == 0) return;*/

		/*foreach (Collider hit in hits) {
			if (hit.CompareTag("Ball") || hit.CompareTag("Player")) continue;
			if (hit.ClosestPoint(targetPos) != targetPos) {
				targetPos.y += Collider.radius;
				break;
			}
        }*/

		/*Vector3 pos = GetPosition();

		RaycastHit[] hits = Physics.CapsuleCastAll(
			pos + h,
			pos - h,
			Collider.radius,
			targetPos - pos,
			Vector3.Distance(targetPos, pos),
			DashLayerMask
		);

		for (int i = hits.Length - 1; i > -1; i--) {
			RaycastHit hit = hits[i];
			foreach (Collider col in colliders) {
				if (hit.collider == col && hit.collider.ClosestPoint(targetPos) != targetPos) {
					Vector3 point = hit.point;
					Vector3 diff = pos - Collider.ClosestPoint(point);
					targetPos = point + diff;
					break;
				}
			}
		}*/
		/*RaycastHit[] hits = Physics.CapsuleCastAll(
			pos + new Vector3(0f, Collider.radius, 0f),
			pos + new Vector3(0f, Collider.height - Collider.radius, 0f),
			Collider.radius - Physics.defaultContactOffset,
			targetPos - pos,
			Vector3.Distance(targetPos, pos)
		);*/
		/*if (hits.Length > 0) {
			RaycastHit hit = hits[hits.Length - 1];
			Debug.Log("First: " + hit.collider.name + ", " + hits.Length);
			if (hit.collider.CompareTag("Ball")) {
				if (hits.Length > 1)
					hit = hits[hits.Length - 2];
				else
					return;
			}
			Debug.Log("Second: " + hit.collider.name);
			if (hit.collider.ClosestPoint(targetPos) == targetPos) {
				Debug.Log("SAME");
				Vector3 point = hit.point;
				Vector3 diff = Collider.ClosestPoint(point) - Collider.transform.position;
				targetPos = point + diff;
			}
		}*/
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
		coreMovement.enabled = false;
		coreMovement.Reinitialize();
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

	public Vector3 GetPosition() => coreMovement.Rigidbody.position;

}
