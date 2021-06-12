using UnityEngine;
using System;

public class CorePlayer : MonoBehaviour {
	public  CoreBall        coreB;
	public  CoreView        coreV;

	public  float           ballSpeed;
	public  float           dashSpeed;

	private CoreMovement    coreM;

	private float           chargeDelta;
	public  float           chargeMax;
	public  float           chargeRate;
	public  float           chargeReset;

	private Vector3     deltaLastInput;
	private Vector3Int  deltaRaises;

	static float        inputDeadzone = 0.05f;

	private bool        dashing;
	private bool        chargeCatch;

	private int         maxFaults = 5;
	private int         deltaFaults;
	private float       safeguard;

	private bool        onGround;

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
		coreM = GetComponent<CoreMovement> ();
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update () {
		Vector3 delta;
		delta.x = Input.GetAxis ( "Horizontal" );
		delta.y = Input.GetAxis ( "Jump" );
		delta.z = Input.GetAxis ( "Vertical" );

		InputTick ( delta );
	
		if ( !dashing ) {
			coreM.InputTick ( deltaRaises );
		} else {
			float deltaClose = ( coreB.transform.position - transform.position ).sqrMagnitude;
			if ( deltaClose > safeguard ) {
				deltaFaults--;
				if ( deltaFaults < 0 ) {
					coreB.Grab ();
					Returned ();
				}
			}
			safeguard = deltaClose;
		}

		if ( Input.GetMouseButtonDown( 0 ) ) {
			if ( coreB.IsDeployed () ) {
				if ( //Physics.Raycast ( transform.position, coreB.transform.position - transform.position ) && 
					!dashing ) {
					dashing = true;
					coreB.Fixate ();
					coreM.Override ( ( coreB.transform.position - transform.position ).normalized * dashSpeed );
					safeguard = ( coreB.transform.position - transform.position ).sqrMagnitude;
					//DEBUG
					Debug.DrawRay ( transform.position, ( coreB.transform.position - transform.position ).normalized, Color.blue, 100000 );
					Debug.DrawRay ( coreB.transform.position-Vector3.up/2, Vector3.up, Color.red, 100000 );
					Debug.DrawRay ( coreB.transform.position-Vector3.right/2, Vector3.right, Color.red, 100000 );
					//Debug.Break ();
				}
			}
		}

		if ( !coreB.IsDeployed () ) {
			if ( Input.GetMouseButtonUp ( 0 ) ) {
				chargeCatch = true;
				if ( chargeDelta > 0 ) {
					deltaFaults = maxFaults;
					if ( chargeDelta > chargeMax ) { chargeDelta = chargeMax; }
					coreB.Deploy ( coreV.GetSpawnPoint () );
					coreB.Deflect ( coreV.GetSpawnDirection () * ( chargeDelta / chargeMax ) * ballSpeed );
					chargeDelta = 0;
				}
			}
			if ( Input.GetMouseButton ( 0 ) && chargeCatch ) {
				chargeDelta += Time.deltaTime * chargeRate;
				if ( chargeDelta > chargeReset ) {
					chargeCatch = false;
					chargeDelta = 0;
				}
			}
			if ( !Physics.Raycast ( transform.position, Vector3.down, 1.5f ) ) {
				coreM.drag = 0.1f;
				coreM.speed = new Vector3 ( 0.001f, 0, 0.001f );
				onGround = false;
			} else {
				coreM.drag = 3;
				coreM.speed = new Vector3 ( 1, 0, 1 );
				onGround = true;
			}
		}
	}

	public void Returned () {
		coreV.EndOverride ();
		coreM.EndOverride ();
		coreM.drag = 3;
		dashing = false;
	}

	private void OnCollisionEnter ( Collision collision ) {
		//coreB.Grab ();
		//Returned ();
	}

}
