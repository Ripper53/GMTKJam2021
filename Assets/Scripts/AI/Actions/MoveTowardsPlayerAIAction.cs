using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class MoveTowardsPlayerAIAction : AIAction {
        public float Speed;

        public override void Execute(AIToken token) {
            Vector3 dir = token.Source.GetToPlayerDirection().normalized;
            token.Source.CoreMovement.InputTick(dir);
        }

    }
}
