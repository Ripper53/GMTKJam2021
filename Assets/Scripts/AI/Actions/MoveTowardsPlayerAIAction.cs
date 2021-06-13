using UnityEngine;

namespace AI {
    public class MoveTowardsPlayerAIAction : AIAction {
        public float Speed;

        public override void Execute(AIToken token) {
            Vector3 dir = token.Source.GetPlayerTowardsDirection().normalized;
            token.Source.CoreMovement.AddVelocity(dir);
        }

    }
}
