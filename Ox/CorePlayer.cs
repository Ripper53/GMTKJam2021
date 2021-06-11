using UnityEngine;

public class CorePlayer : MonoBehaviour {
	private CoreMovement    coreM;

	private Vector3     deltaLastInput;

	static float        inputDeadzone;

	public void InputTick ( Vector3 a1 ) {
		Vector3Int deltaRaises = Vector3Int.zero;
		deltaRaises.x = -1;
		if ( Modulus ( a1.x ) > Modulus ( deltaLastInput.x ) ) { deltaRaises.x = 1; } else {
			if ( Modulus ( a1.x - deltaLastInput.x ) < inputDeadzone ) { deltaRaises.x = 0; }
		}
		deltaRaises.y = -1;
		if ( Modulus ( a1.y ) > Modulus ( deltaLastInput.y ) ) { deltaRaises.y = 1; } else {
			if ( Modulus ( a1.y - deltaLastInput.y ) < inputDeadzone ) { deltaRaises.y = 0; }
		}
		deltaRaises.z = -1;
		if ( Modulus ( a1.z ) > Modulus ( deltaLastInput.z ) ) { deltaRaises.z = 1; } else {
			if ( Modulus ( a1.z - deltaLastInput.z ) < inputDeadzone ) { deltaRaises.z = 0; }
		}
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
		coreM.InputTick ( delta );
	}

	float Modulus ( float a1 ) {
		if ( a1 < 0 ) return -a1;
		return a1;
	}
}
