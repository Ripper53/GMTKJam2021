using UnityEngine;

namespace AI {
    public class ShootProjectileAtPlayerAIAction : ShootProjectileAIAction {

        protected override Vector3 GetDirection(AIToken token) {
            return token.Source.GetDirectionTowardsPlayer().normalized;
        }

    }
}
