using System.Collections;
using UnityEngine;

namespace AI {
    public abstract class ShootProjectileAIAction : AIAction {
        public Rigidbody ProjectilePrefab;
        public Transform Origin;
        public float Speed;
        public float Cooldown;

        private bool isOn = true;

        public override void Execute(AIToken token) {
            if (!isOn) return;
            isOn = false;
            StartCoroutine(StartCooldown());
            Rigidbody projectile = Instantiate(ProjectilePrefab, Origin.position, Origin.rotation);
            projectile.gameObject.SetActive(true);
            projectile.velocity = GetDirection(token) * Speed;
        }

        protected abstract Vector3 GetDirection(AIToken token);

        private IEnumerator StartCooldown() {
            yield return new WaitForSeconds(Cooldown);
            isOn = true;
        }

    }
}
