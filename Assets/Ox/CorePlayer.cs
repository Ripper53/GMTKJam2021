using UnityEngine;
using System;

public class CorePlayer : MonoBehaviour {
	public  CoreBall        coreB;
	public  CoreView        coreV;

	public  float           ballSpeed;
	public  float           dashSpeed;

	private CoreMovement    coreM;

	private Vector3     deltaLastInput;
	private Vector3Int  deltaRaises;

	static float        inputDeadzone = 0.05f;

	private bool        dashing;

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
		}

		if ( Input.GetMouseButtonDown( 0 ) ) {
			if ( coreB.IsDeployed () ) {
				if ( //Physics.Raycast ( transform.position, coreB.transform.position - transform.position ) && 
					!dashing ) {
					dashing = true;
					coreB.Fixate ();
					coreV.Override ( coreB.transform.position );
					coreM.Override ( ( coreB.transform.position - transform.position ).normalized * dashSpeed );
				}
			} else {
				coreB.Deploy ( coreV.GetSpawnPoint() );
				coreB.Deflect ( coreV.GetSpawnDirection() * ballSpeed );
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
		coreB.Grab ();
		Returned ();
	}

}
