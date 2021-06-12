using System.Collections;
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

	public delegate void ChangedChargeDeltaAction(CorePlayer source, float chargeDelta);
	public event ChangedChargeDeltaAction ChangedChargeDelta;
	private float chargeDelta;
	private float ChargeDelta {
		get => chargeDelta;
		set {
			chargeDelta = value;
			ChangedChargeDelta?.Invoke(this, chargeDelta);
        }
    }
	public float ChargeMax;
	public float ChargeRate;

	private bool dashing;
	private bool chargeCatch;

	protected void Start() {
		coreMovement = GetComponent<CoreMovement>();
	}

	private bool throwBall = false;
	protected void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if (dashing) {
				chargeCatch = true;
			} else if (CoreBall.IsDeployed()) {
				BeginDash();
				CoreBall.Fixate();
			} else {
				chargeCatch = true;
			}
		} else if (Input.GetMouseButtonUp(0) && chargeCatch) {
			throwBall = true;
			chargeCatch = false;
		} else if (chargeCatch) {
			ChargeDelta += Time.deltaTime * ChargeRate;
			if (ChargeDelta > ChargeMax)
				ChargeDelta = ChargeMax;
		}
	}

	protected void FixedUpdate() {
		if (dashing) {
			DashToBall();
			return;
		}

		Vector3 delta = new Vector3(
			Input.GetAxis("Horizontal"),
			0f,
			Input.GetAxis("Vertical")
		).normalized;
		Vector3 vel = coreMovement.Rigidbody.velocity;
		float magnitude = new Vector2(vel.x, vel.z).magnitude;
		Debug.Log(magnitude);
		if (magnitude < MoveSpeed)
			coreMovement.AddVelocity(delta * MoveSpeed);
		else {
			coreMovement.AddVelocity(delta * magnitude);
		}

		if (throwBall) {
			throwBall = false;
			if (!CoreBall.IsDeployed()) {
				CoreBall.Deploy(CoreView.GetSpawnPoint(), CoreView.GetSpawnDirection() * (ChargeDelta / ChargeMax) * BallSpeed);
				ChargeDelta = 0f;
			}
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

		Vector3 targetPos;
		if (CoreBallContact.InContact)
			targetPos = CoreBallContact.Contact;
		else
			targetPos = CoreBall.GetPosition();
		Transform.position = Vector3.Lerp(dashOrigin, targetPos, dashFill);
	}

	public void Returned() {
		CoreView.EndOverride();
		EndDash();
		dashing = false;
	}

	public delegate void DashAction(CorePlayer source);
	public event DashAction Dash;
	private Vector3 dashDir;
	private void BeginDash() {
		dashing = true;
		coreMovement.Rigidbody.detectCollisions = false;
		coreMovement.Rigidbody.isKinematic = true;
		coreMovement.enabled = false;
		coreMovement.Reinitialize();
		dashFill = 0f;
		dashOrigin = Transform.position;
		dashDir = (CoreBall.GetPosition() - dashOrigin).normalized;
		Dash?.Invoke(this);
	}
	private void EndDash() {
		CoreBall.ForceGrab();
		coreMovement.Rigidbody.detectCollisions = true;
		coreMovement.Rigidbody.isKinematic = false;
		coreMovement.enabled = true;
		coreMovement.Rigidbody.velocity = dashDir * DashSpeed;
		dashing = false;
	}

	public Vector3 GetPosition() => coreMovement.Rigidbody.position;

}
