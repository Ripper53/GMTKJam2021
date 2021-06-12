using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public abstract class ConditionalAIAction : AIAction {
        public AIAction Action;

        public abstract bool Condition(AIToken token);

        public override void Execute(AIToken token) {
            if (Condition(token)) {
                Action.Execute(token);
                token.Cancel();
            }
        }

    }
}
