using UnityEngine;

public class CoreView : MonoBehaviour {
    public    Transform     target;
        private     Transform       rootX;
        private     Transform       rootZ;

    public  Rigidbody       targetRb;

    void Start () {
        if( target != null ) {
            rootZ = target.GetChild ( 0 );
            rootX = rootZ.GetChild ( 0 );
        }    
    }

	public  Vector2     aSensitivity;
	public  Vector2     aStrength;

	private  Vector2     d1;
	private  Vector2     d2;
	
	void Update () {
		d1.y += Input.GetAxis("Mouse X") * aSensitivity.y;
		d1.x += Input.GetAxis("Mouse Y") * aSensitivity.x;

		d2.y += d1.y * aStrength.y;
		d2.x -= d1.x * aStrength.x;
		
		if ( d2.x > 90 ) { d1.x -= ( 90 - d2.x ) / aStrength.x; d2.x = 90; }
		if ( d2.x < -90 ) { d1.x -= ( -90 - d2.x ) / aStrength.x; d2.x = -90; }

		target.transform.Rotate ( 0, d1.y * aStrength.y, 0 );	d1.y *= 1 - aStrength.y;
		rootX.transform.Rotate ( -d1.x * aStrength.x, 0, 0 );	d1.x *= 1 - aStrength.x;
	}

	private void FixedUpdate () {
		targetRb.transform.rotation = target.rotation;
	}
}

