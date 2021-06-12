using UnityEngine;

namespace AI {
    public class DashRelativeToPlayerAIAction : DashAIAction {
        [Tooltip("Dashes towards the player with the direction offset by this amount (in degrees). To the right by direction offset, then to the left, then right, then left, ect...")]
        public Vector3 DirectionOffset;

        private float dir = -1f;

        protected override Vector3 GetDirection(AIToken token) {
            dir = -dir;
            return Quaternion.Euler(DirectionOffset * dir) * token.Source.GetToPlayerDirection().normalized;
        }

    }
}
