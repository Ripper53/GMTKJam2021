using UnityEngine;

public class DragWhenGrounded : MonoBehaviour {
    public Rigidbody Rigidbody;
    public GroundCheck GroundCheck;
    public float Drag;

    protected void FixedUpdate() {
        Rigidbody.drag = GroundCheck.Evaluate() ? Drag : 0f;
    }

}
