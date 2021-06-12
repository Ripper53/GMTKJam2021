using UnityEngine;

public class Lock : MonoBehaviour {
    public  Transform   target;
    public  float       strength;

    void Update () {
        transform.position = Vector3.Lerp ( transform.position, target.position, strength );
    }
}
