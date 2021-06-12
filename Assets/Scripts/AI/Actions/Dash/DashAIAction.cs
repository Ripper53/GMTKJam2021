using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class DashAIAction : AIAction {
        [Tooltip("The speed of the dash, in combination with its time will result in different dash distances.")]
        public float Speed;
        [Tooltip("How long to dash for?")]
        public float Time;
        public float Cooldown;

        private bool isOn = true;

        public override void Execute(AIToken token) {
            if (!isOn) return;
            isOn = false;

            Vector3 dir = GetDirection(token) * Speed;    // Get the direction of the dash.
            StartCoroutine(CooldownTimer(token, dir)); // Start cooldown.

            token.Source.CoreMovement.InputTick(dir);
        }

        protected virtual Vector3 GetDirection(AIToken token) => token.Source.GetToPlayerDirection().normalized;

        private IEnumerator CooldownTimer(AIToken token, Vector3 dir) {
            Coroutine dashCoroutine = StartCoroutine(ConstantDash(token, dir));
            yield return new WaitForSeconds(Time);
            StopCoroutine(dashCoroutine);
            yield return new WaitForSeconds(Cooldown);
            isOn = true;
        }

        private IEnumerator ConstantDash(AIToken token, Vector3 dir) {
            while (true) {
                yield return new WaitForFixedUpdate();
                token.Source.CoreMovement.InputTick(dir);
            }
        }

    }
}
