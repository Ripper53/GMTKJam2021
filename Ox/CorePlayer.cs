using UnityEngine;
using System;

public class CorePlayer : MonoBehaviour {
	private CoreMovement    coreM;

	private Vector3     deltaLastInput;
	private Vector3Int  deltaRaises;

	static float        inputDeadzone = 0.05f;

	private int RaiseFunction ( float a1, float a2 ) {
		if ( Math.Abs ( a1 ) > Math.Abs ( a2 ) ) { return 1 * Math.Sign( a1 ); }
		
		if ( Math.Abs ( a1 - a2 ) < inputDeadzone ) { return ( int ) Math.Round( a1, 0 ); }
	
		return -1;
	}

	public void InputTick ( Vector3 a1 ) {
		deltaRaises.x = RaiseFunction ( a1.x, deltaLastInput.x );
		deltaRaises.y = RaiseFunction ( a1.y, deltaLastInput.y );
		deltaRaises.z = RaiseFunction ( a1.z, deltaLastInput.z );
		deltaLastInput = a1;
	}

	void Start () {
		coreM = GetComponent<CoreMovement> ();
	}

	void Update () {
		Vector3 delta;
		delta.x = Input.GetAxis ( "Horizontal" );
		delta.y = Input.GetAxis ( "Jump" );
		delta.z = Input.GetAxis ( "Vertical" );

		InputTick ( delta );

		delta = Vector3.zero;

		coreM.InputTick ( deltaRaises );
	}

	float Modulus ( float a1 ) {
		if ( a1 < 0 ) return -a1;
		return a1;
	}
}
