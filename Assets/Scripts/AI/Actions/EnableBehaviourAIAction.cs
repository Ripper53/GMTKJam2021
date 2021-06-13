using System.Collections;
using UnityEngine;

namespace AI {
    public class EnableBehaviourAIAction : AIAction {
        public Behaviour Behaviour;
        public float Time;

        private Coroutine disableCoroutine = null;

        public override void Execute(AIToken token) {
            Behaviour.enabled = true;
            if (disableCoroutine != null)
                StopCoroutine(disableCoroutine);
            disableCoroutine = StartCoroutine(Disable());
        }

        private IEnumerator Disable() {
            yield return new WaitForSeconds(Time);
            Behaviour.enabled = false;
        }

    }
}
