using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePlayer : MonoBehaviour {
	public Transform Transform;
	public CapsuleCollider Collider;
	public LayerMask DashLayerMask, EnemyLayerMask;
	public float HitRadius;
	public CoreBall CoreBall;
	public CoreBallContact CoreBallContact;
	public CoreView CoreView;

	public float MoveSpeed;
	public float BallSpeed;
	public float DashSpeed;
	public float DashMomentum;
	public float SlowMotionScale;
	public float SlowMotionTime;

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
		if (dashing)
			DashToBall();

		if (Input.GetMouseButtonDown(0)) {
			if (dashing) {
				chargeCatch = true;
			} else if (CoreBall.IsDeployed()) {
				CoreBall.Fixate();
				BeginDash();
			} else {
				chargeCatch = true;
			}
		} else if (Input.GetMouseButtonUp(0) && chargeCatch) {
			throwBall = true;
			chargeCatch = false;
		} else if (chargeCatch) {
			ChargeDelta += Time.unscaledDeltaTime * ChargeRate;
			if (ChargeDelta > ChargeMax)
				ChargeDelta = ChargeMax;
		}
	}

	public delegate void ThrowedAction(CorePlayer source);
	public event ThrowedAction Throwed;
	protected void FixedUpdate() {
		if (dashing) return;

		Vector3 delta = new Vector3(
			Input.GetAxis("Horizontal"),
			0f,
			Input.GetAxis("Vertical")
		).normalized;
		Vector3 vel = coreMovement.Rigidbody.velocity;
		float magnitude = new Vector2(vel.x, vel.z).magnitude;
		if (magnitude < MoveSpeed)
			coreMovement.AddVelocity(delta * MoveSpeed);
		//else
		//	coreMovement.AddVelocity(delta * magnitude);

		if (throwBall) {
			throwBall = false;
			if (!CoreBall.IsDeployed()) {
				CoreBall.Deploy(CoreView.GetSpawnPoint(), CoreView.GetSpawnDirection() * (ChargeDelta / ChargeMax) * BallSpeed);
				ChargeDelta = 0f;
				Throwed?.Invoke(this);
			}
		}
	}

    private Vector3 dashOrigin;
	private float dashFill;
	private Coroutine slowMotionCoroutine = null;
	private void DashToBall() {
		dashFill += DashSpeed * Time.deltaTime;
		if (dashFill >= 1f) {
			dashFill = 1f;
			EndDash();
        }

		if (toKill.Count > 0) {
			ToKill ai = toKill.Peek();
			while (ai.Time <= dashFill) {
				ai = toKill.Dequeue();
				if (slowMotionCoroutine != null)
					StopCoroutine(slowMotionCoroutine);
				Time.timeScale = SlowMotionScale;
				Killed?.Invoke(this, ai.AI);
				StartCoroutine(KillEnemy(ai.AI));
				slowMotionCoroutine = StartCoroutine(SlowMotion());
				if (toKill.Count == 0) break;
				ai = toKill.Peek();
			}
		}

		Vector3 targetPos;
		if (CoreBallContact.InContact)
			targetPos = CoreBallContact.Contact;
		else
			targetPos = CoreBall.GetPosition();
		Transform.position = Vector3.Lerp(dashOrigin, targetPos, dashFill);
	}
	private IEnumerator KillEnemy(ArtificialIntelligence ai) {
		yield return new WaitForSecondsRealtime(SlowMotionTime);
		ai.Kill();
    }
	private IEnumerator SlowMotion() {
		yield return new WaitForSecondsRealtime(SlowMotionTime);
		Time.timeScale = 1f;
    }

	public void Returned() {
		CoreView.EndOverride();
		EndDash();
		dashing = false;
	}

	public delegate void DashAction(CorePlayer source);
	public event DashAction Dash;
	private Vector3 dashDir;
	public delegate void KilledAction(CorePlayer source, ArtificialIntelligence ai);
	public event KilledAction Killed;
	private readonly Queue<ToKill> toKill = new Queue<ToKill>();
	private class ToKill {
		public ArtificialIntelligence AI { get; }
		public float Time { get; }
		public ToKill(ArtificialIntelligence ai, float time) {
			AI = ai;
			Time = time;
        }
    }
	private void BeginDash() {
		dashing = true;
		coreMovement.Rigidbody.detectCollisions = false;
		coreMovement.Rigidbody.isKinematic = true;
		coreMovement.enabled = false;
		coreMovement.Reinitialize();
		dashFill = 0f;
		dashOrigin = GetPosition();
		Vector3 targetPos = CoreBall.GetPosition();
		dashDir = (targetPos - dashOrigin).normalized;

		float dis = Vector3.Distance(dashOrigin, targetPos);
		RaycastHit[] enemyHits = Physics.SphereCastAll(dashOrigin, HitRadius, dashDir, dis, EnemyLayerMask);
		foreach (RaycastHit enemyHit in enemyHits) {
			if (enemyHit.collider.TryGetComponent(out ArtificialIntelligence ai)) {
				ai.Disable();
				toKill.Enqueue(new ToKill(ai, enemyHit.distance / dis));
			}
        }

		Dash?.Invoke(this);
	}
	private void EndDash() {
		CoreBall.ForceGrab();
		coreMovement.Rigidbody.detectCollisions = true;
		coreMovement.Rigidbody.isKinematic = false;
		coreMovement.enabled = true;
		coreMovement.Rigidbody.velocity = dashDir * DashMomentum;
		dashing = false;
	}

	public Vector3 GetPosition() => coreMovement.Rigidbody.position;

}
