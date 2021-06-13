using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour {
    public Rigidbody Rigidbody;
    public float Speed, Time;

    private bool moving = false;
    private bool goingForward = true;
    private float speed;

    protected void FixedUpdate() {
        if (moving) {
            Rigidbody.MovePosition(Rigidbody.position + (Rigidbody.rotation * new Vector3(0f, 0f, speed * UnityEngine.Time.fixedDeltaTime)));
        } else {
            moving = true;
            StartCoroutine(Cooldown());
            if (goingForward) {
                goingForward = false;
                speed = Speed;
            } else {
                goingForward = true;
                speed = -Speed;
            }
        }
    }

    private IEnumerator Cooldown() {
        yield return new WaitForSeconds(Time);
        moving = false;
    }

}
