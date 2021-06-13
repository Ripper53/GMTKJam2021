using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class DashIntoTrajectoryOfBallAIAction : AIAction {
        public DirectionalDashAIAction DashAction;
        public float DetectionRadius, DetectionDistance;

        public override void Execute(AIToken token) {
            Vector3 dir = token.Source.Ball.GetTrajectory();

            Vector3 ballPos = token.Source.Ball.GetPosition();

            if (CheckIfInCone(ballPos, dir, token.Source.Rigidbody.position)) {
#if UNITY_EDITOR
                Debug.Log("RE");
#endif
                token.Source.Transform.forward = new Vector3(-dir.x, 0f, -dir.z);
                /*Vector3 rotation = token.Source.Transform.eulerAngles;
                rotation.y = (Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg) + 180f;
                token.Source.Transform.rotation = Quaternion.Euler(rotation);*/

                Vector3 predictPos = ballPos + (dir * 10f);

                //dir = predictPos - token.Source.Rigidbody.position;
                dir = new Vector3(-dir.z, 0f, dir.x);
                Vector3 diff = ballPos - token.Source.Rigidbody.position;
                if (diff.x < 0f ||
                    diff.z < 0f)
                    dir.z = -dir.z;
                //dir.y = 0f;

                DashAction.Direction = token.Source.Transform.InverseTransformDirection(dir);
                DashAction.Execute(token);
            }

        }

        private bool CheckIfInCone(Vector3 coneOrigin, Vector3 coneDir, Vector3 position) {
            float coneDis = Vector3.Dot(position - coneOrigin, coneDir);
            if (coneDis >= 0f && coneDis <= DetectionDistance) {
                float coneRad = (coneDis / DetectionDistance) * DetectionRadius;
                float orthDis = ((position - coneOrigin) - (coneDis * coneDir)).magnitude;
                return orthDis < coneRad;
            }
            return false;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.magenta;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawLine(Vector3.zero, Vector3.right * DetectionDistance);
            float y = DetectionRadius * DetectionDistance;
            float hAngle = Mathf.Atan2(y, DetectionDistance);
            Vector3 hDir = new Vector3(Mathf.Cos(hAngle), Mathf.Sin(hAngle)) * Mathf.Sqrt((DetectionDistance * DetectionDistance) + (y * y));
            Gizmos.DrawLine(Vector3.zero, new Vector3(hDir.x, hDir.z, hDir.y));
            Gizmos.DrawLine(Vector3.zero, new Vector3(hDir.x, hDir.z, -hDir.y));
            Gizmos.DrawLine(Vector3.zero, new Vector3(hDir.x, hDir.y, hDir.z));
            Gizmos.DrawLine(Vector3.zero, new Vector3(hDir.x, -hDir.y, hDir.z));
            //Gizmos.DrawWireSphere(dir * DetectionDistance, DetectionRadius);
        }
#endif

    }
}
