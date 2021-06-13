using UnityEngine;

public class DeactivateOnTrigger : MonoBehaviour {
    public GameObject GameObject;

    protected void OnTriggerEnter(Collider other) {
        GameObject.SetActive(false);
    }

}
