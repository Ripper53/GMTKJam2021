using System.Collections;
using UnityEngine;

public class CoreBall : MonoBehaviour {
    public CorePlayer TargetPlayer;
    public GameObject GameObject;
    private Transform baseT;
    
    private Rigidbody target;
    
    private bool deployed;

    void Start () {
        target = GetComponent<Rigidbody> ();
        baseT = transform.parent;
        Grab();
    }

    public delegate void GrabbedAction(CoreBall source);
    public event GrabbedAction Grabbed;
    private bool canGrab = true;
    public bool Grab() {
        if (!canGrab) return false;
        ForceGrab();
        return true;
	}
    public void ForceGrab() {
        GameObject.SetActive(false);
        deployed = false;
        transform.SetParent(TargetPlayer.Transform);
        transform.localPosition = Vector3.zero;
        Grabbed?.Invoke(this);
    }

    public void Fixate() {
        target.isKinematic = true;
        target.velocity = Vector3.zero;
    }

    public delegate void DeployedAction(CoreBall source);
    public event DeployedAction Deployed;
    public void Deploy ( Vector3 pos, Vector3 vel ) {
        canGrab = false;
        TargetPlayer.StartCoroutine(GrabCooldown());
        deployed = true;
        target.isKinematic = false;
        transform.SetParent(baseT);
        transform.position = pos;
        GameObject.SetActive(true);
        target.velocity = vel;
        Deployed?.Invoke(this);
    }

	private void OnCollisionStay ( Collision collision ) {
        if (collision.gameObject.CompareTag(TargetPlayer.tag) && Grab()) {
            TargetPlayer.Returned();
        } else {
           // Vector3.Project ( collision.GetContact ( 0 ).normal );
        }
	}

    public bool IsDeployed () {
        return deployed;
    }

    public Vector3 GetPosition() => transform.position;

    private IEnumerator GrabCooldown() {
        yield return new WaitForSeconds(0.1f);
        canGrab = true;
    }

}
