using UnityEngine;
using System;

public class CorePlayer : MonoBehaviour {
	public CoreBall CoreBall;
	public CoreView CoreView;

	public float BallSpeed;
	public float DashSpeed;

	private CoreMovement coreMovement;

	private float chargeDelta;
	public float ChargeMax;
	public float ChargeRate;
	public float ChargeReset;

	private Vector3 deltaLastInput;
	private Vector3Int deltaRaises;

	static float inputDeadzone = 0.05f;

	private bool dashing;
	private bool chargeCatch;

	private int maxFaults = 5;
	private int deltaFaults;
	private float safeguard;

	private bool onGround;

	private int RaiseFunction ( float a1, float a2 ) {
		if ( Math.Abs ( a1 ) > Math.Abs ( a2 ) ) { return 1 * Math.Sign( a1 ); }
		
		if ( Math.Abs ( a1 - a2 ) < inputDeadzone ) { return ( int ) Math.Round( a1, 0 ); }
	
		return -1;
	}

	private void InputTick ( Vector3 a1 ) {
		deltaRaises.x = RaiseFunction ( a1.x, deltaLastInput.x );
		deltaRaises.y = RaiseFunction ( a1.y, deltaLastInput.y );
		deltaRaises.z = RaiseFunction ( a1.z, deltaLastInput.z );
		deltaLastInput = a1;
	}

	void Start () {
		coreMovement = GetComponent<CoreMovement> ();
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update () {
		Vector3 delta;
		delta.x = Input.GetAxis ( "Horizontal" );
		delta.y = Input.GetAxis ( "Jump" );
		delta.z = Input.GetAxis ( "Vertical" );

		InputTick ( delta );
	
		if ( !dashing ) {
			coreMovement.InputTick ( deltaRaises );
		} else {
			float deltaClose = ( CoreBall.transform.position - transform.position ).sqrMagnitude;
			if ( deltaClose > safeguard ) {
				deltaFaults--;
				if ( deltaFaults < 0 ) {
					CoreBall.Grab ();
					Returned ();
				}
			}
			safeguard = deltaClose;
		}

		if ( Input.GetMouseButtonDown( 0 ) ) {
			if ( CoreBall.IsDeployed () ) {
				if ( //Physics.Raycast ( transform.position, coreB.transform.position - transform.position ) && 
					!dashing ) {
					dashing = true;
					CoreBall.Fixate ();
					coreMovement.Override ( ( CoreBall.transform.position - transform.position ).normalized * DashSpeed );
					safeguard = ( CoreBall.transform.position - transform.position ).sqrMagnitude;
#if UNITY_EDITOR
					//DEBUG
					Debug.DrawRay ( transform.position, ( CoreBall.transform.position - transform.position ).normalized, Color.blue, 100000 );
					Debug.DrawRay ( CoreBall.transform.position-Vector3.up/2, Vector3.up, Color.red, 100000 );
					Debug.DrawRay ( CoreBall.transform.position-Vector3.right/2, Vector3.right, Color.red, 100000 );
					//Debug.Break ();
#endif
				}
			}
		}

		if ( !CoreBall.IsDeployed () ) {
			if ( Input.GetMouseButtonUp ( 0 ) ) {
				chargeCatch = true;
				if ( chargeDelta > 0 ) {
					deltaFaults = maxFaults;
					if ( chargeDelta > ChargeMax ) { chargeDelta = ChargeMax; }
					CoreBall.Deploy ( CoreView.GetSpawnPoint () );
					CoreBall.Deflect ( CoreView.GetSpawnDirection () * ( chargeDelta / ChargeMax ) * BallSpeed );
					chargeDelta = 0;
				}
			}
			if ( Input.GetMouseButton ( 0 ) && chargeCatch ) {
				chargeDelta += Time.deltaTime * ChargeRate;
				if ( chargeDelta > ChargeReset ) {
					chargeCatch = false;
					chargeDelta = 0;
				}
			}
			if ( !Physics.Raycast ( transform.position, Vector3.down, 1.5f ) ) {
				coreMovement.Drag = 0.1f;
				coreMovement.Speed = 0.001f;
				onGround = false;
			} else {
				coreMovement.Drag = 3;
				coreMovement.Speed = 1f;
				onGround = true;
			}
		}
	}

	public void Returned () {
		CoreView.EndOverride ();
		coreMovement.EndOverride ();
		coreMovement.Drag = 3;
		dashing = false;
	}

	private void OnCollisionEnter ( Collision collision ) {
		//coreB.Grab ();
		//Returned ();
	}

}
