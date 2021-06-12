using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class DistanceConditionalAIAction : ConditionalAIAction {
        public float Distance;

        public override bool Condition(AIToken token) {
            return Vector3.Distance(token.Source.Rigidbody.position, token.Source.GetPlayerPosition()) < Distance;
        }

    }
}
