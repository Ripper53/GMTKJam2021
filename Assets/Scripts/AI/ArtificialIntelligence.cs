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
            // TO DO
            return new Vector3(0f, 0f, 0f);
        }

        public Vector3 GetBallPosition() {
            // TO DO
            return new Vector3(0f, 0f, 0f);
        }

        /// <summary>
        /// The direction the player is, NOT NORMALIZED!
        /// </summary>
        public Vector3 GetToPlayerDirection() {
            // TO DO
            return GetPlayerPosition() - Rigidbody.position;
        }

    }
}
