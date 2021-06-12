using UnityEngine;

public class Lock : MonoBehaviour {
    public Transform Target;
    public float Strength;

    void Update() {
        transform.position = Vector3.Lerp(transform.position, Target.position, Strength);
    }
}
