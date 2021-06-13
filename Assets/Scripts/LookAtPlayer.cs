using UnityEngine;
using AI;

public class LookAtPlayer : MonoBehaviour {
    public Transform Transform;
    public ArtificialIntelligence AI;

    protected void LateUpdate() {
        Transform.LookAt(AI.Player.GetPosition());
    }

}
