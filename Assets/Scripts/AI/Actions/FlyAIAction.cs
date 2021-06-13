using UnityEngine;

namespace AI {
    public class FlyAIAction : AIAction {
        public float Speed;
        public float BopSpeed;
        public float Height;

        private float x = 0f;

        public override void Execute(AIToken token) {
            x = (x + BopSpeed * token.DeltaTime) % (2f * Mathf.PI);
            float y = Mathf.Sin(x) * (BopSpeed * Height);
            token.Source.CoreMovement.AddVelocity((token.Source.GetDirectionTowardsPlayer().normalized * Speed) + new Vector3(0f, y, 0f));
        }

    }
}
