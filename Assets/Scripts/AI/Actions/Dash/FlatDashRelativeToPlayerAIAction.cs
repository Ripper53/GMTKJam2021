using UnityEngine;

namespace AI {
    public class FlatDashRelativeToPlayerAIAction : DashRelativeToPlayerAIAction {

        protected override Vector3 GetDirection(AIToken token) {
            Vector3 dir = base.GetDirection(token);
            return new Vector3(dir.x, 0f, dir.z);
        }

    }
}
