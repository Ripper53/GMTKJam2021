using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class DashIntoTrajectoryOfBallAIAction : AIAction {

        public override void Execute(AIToken token) {
            Vector3 dir = GetBallTrajectory();



        }

        private Vector3 GetBallTrajectory() => new Vector3(0f, 0f, 1f);

    }
}
