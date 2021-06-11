using UnityEngine;

public class CoreBall : MonoBehaviour {
    public  Transform   targetP;
    private Transform   baseT;
    
    private Rigidbody   target;
    
    private     bool    deployed;

    void Start () {
        target = GetComponent<Rigidbody> ();
        baseT = transform.parent;
        Grab ();
    }

    public void Grab() {
        deployed = false;
        target.velocity = Vector3.zero;
        transform.SetParent ( targetP );
        transform.localPosition = Vector3.zero;
	}

    public void Fixate() {
        target.isKinematic = true;
        target.velocity = Vector3.zero;
    }

    public void Deflect ( Vector3 a1 ) {
        target.AddForce ( a1, ForceMode.VelocityChange );
    }

    public void Deploy ( Vector3 a1 ) {
        deployed = true;
        target.isKinematic = false;
        transform.SetParent ( baseT );
        transform.position = a1;
	}

	private void OnCollisionEnter ( Collision collision ) {
        if ( collision.gameObject.tag == targetP.tag ) {
            Grab ();
            targetP.GetComponent<CorePlayer> ().Returned ();
        }
	}

    public bool IsDeployed () {
        return deployed;
    }
}
