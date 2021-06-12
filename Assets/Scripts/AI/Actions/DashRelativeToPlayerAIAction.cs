using System.Collections;
using UnityEngine;

namespace AI {
    public class DashRelativeToPlayerAIAction : AIAction {
        [Tooltip("The speed of the dash, in combination with its time will result in different dash distances.")]
        public float Speed;
        [Tooltip("How long to dash for?")]
        public float Time;
        public float Cooldown;
        [Tooltip("Dashes towards the player with the direction offset by this amount (in degrees). To the right by direction offset, then to the left, then right, then left, ect...")]
        public Vector3 DirectionOffset;

        private bool isOn = true;
        private float dir = -1f;

        public override void Execute(AIToken token) {
            if (!isOn) return;
            isOn = false;
            this.dir = -this.dir;
            StartCoroutine(CooldownTimer(token));

            Vector3 dir = Quaternion.Euler(DirectionOffset * this.dir) * token.Source.GetToPlayerDirection().normalized;

            token.Source.CoreMovement.InputTick(dir * Speed);
        }

        private IEnumerator CooldownTimer(AIToken token) {
            yield return new WaitForSeconds(Time);
            token.Source.Rigidbody.velocity = Vector3.zero;
            yield return new WaitForSeconds(Cooldown);
            isOn = true;
        }

    }
}
