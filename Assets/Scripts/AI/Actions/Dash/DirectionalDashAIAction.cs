using UnityEngine;

namespace AI {
    public class DirectionalDashAIAction : DashAIAction {
        public Vector3 Direction;

        protected override Vector3 GetDirection(AIToken token) {
            return Direction.normalized;
        }

    }
}
