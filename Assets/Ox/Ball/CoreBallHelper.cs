using UnityEngine;

public class CoreBallHelper : MonoBehaviour {
    public CoreBall CoreBall;
    public CorePlayer CorePlayer;
    public Transform GroundHelper;
    public Transform LinkHelper;

    protected void LateUpdate() {
        Vector3 ballPos = CoreBall.GetPosition(), playerPos = CorePlayer.GetPosition(), diff = playerPos - ballPos;

        #region Ground
        const float maxDistance = 1000f;
        Vector3 scale = GroundHelper.localScale;
        if (Physics.Raycast(ballPos, Vector3.down, out RaycastHit hit)) {
            scale.y = hit.distance;
        } else {
            scale.y = maxDistance;
        }

        Vector3 pos = GroundHelper.localPosition;
        pos.y = -scale.y / 2f;
        GroundHelper.localPosition = pos;

        GroundHelper.localScale = scale;
        #endregion

        #region Player Link
        LinkHelper.LookAt(playerPos);
        pos = ballPos + (diff / 2f);
        LinkHelper.position = pos;
        scale = LinkHelper.localScale;
        scale.z = Vector3.Distance(playerPos, ballPos);
        LinkHelper.localScale = scale;
        #endregion
    }

}
