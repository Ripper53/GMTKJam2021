using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class ArtificialIntelligence : MonoBehaviour {
        [Header("World")]
        public CorePlayer Player;
        public CoreBall Ball;
        [Header("Self")]
        public Transform Transform;
        public Rigidbody Rigidbody;
        public CoreMovement CoreMovement;
        public AIAction[] Actions;

        protected void FixedUpdate() {
            AIToken token = new AIToken(this, Time.fixedDeltaTime);
            foreach (AIAction action in Actions) {
                action.Execute(token);
                if (token.Canceled)
                    return;
            }
        }

        public Vector3 GetPlayerPosition() {
            return Player.GetPosition();
        }

        public Vector3 GetBallPosition() {
            return Ball.GetPosition();
        }

        /// <summary>
        /// The direction the player is, NOT NORMALIZED!
        /// </summary>
        public Vector3 GetPlayerTowardsDirection() {
            return GetPlayerPosition() - Rigidbody.position;
        }

        public void Disable() {
            enabled = false;
            Rigidbody.detectCollisions = false;
            Rigidbody.isKinematic = true;
            Rigidbody.velocity = Vector3.zero;
        }

        public delegate void KilledEvent(ArtificialIntelligence source);
        public event KilledEvent Killed;
        public void Kill() {
            gameObject.SetActive(false);
            Killed?.Invoke(this);
        }

    }
}
