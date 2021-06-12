using System.Collections;
using UnityEngine;

namespace AI {
    public abstract class DashAIAction : AIAction {
        [Tooltip("The speed of the dash, in combination with its time will result in different dash distances.")]
        public float Speed;
        [Tooltip("How long to dash for?")]
        public float Time;
        public float Cooldown;

        private bool isOn = true;

        public override void Execute(AIToken token) {
            if (!isOn) return;
            isOn = false;

            // Get the direction of the dash.
            Vector3 dir = GetDirection(token) * Speed;

            // Start cooldown.
            StartCoroutine(CooldownTimer(token, dir));

            token.Source.CoreMovement.AddVelocity(dir);
        }

        /// <returns>The direction of the dash.</returns>
        protected abstract Vector3 GetDirection(AIToken token);

        private IEnumerator CooldownTimer(AIToken token, Vector3 dir) {
            // Continue to apply movement so A.I. does not slow down during the dash, only after it.
            Coroutine dashCoroutine = StartCoroutine(ConstantDash(token, dir));
            yield return new WaitForSeconds(Time);
            // Stop applying movement, allowing the A.I. to slow down.
            StopCoroutine(dashCoroutine);
            yield return new WaitForSeconds(Cooldown);
            isOn = true;
        }

        private IEnumerator ConstantDash(AIToken token, Vector3 dir) {
            while (true) {
                yield return new WaitForFixedUpdate();
                token.Source.CoreMovement.AddVelocity(dir);
            }
        }

    }
}
