using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBallHelper : MonoBehaviour {
    public Transform Origin;
    public Transform GroundHelper;

    protected void Update() {
        const float maxDistance = 100f;
        Vector3 scale = GroundHelper.localScale;
        if (Physics.Raycast(Origin.position, Vector3.down, out RaycastHit hit, maxDistance)) {
            scale.y = hit.distance;
        } else {
            scale.y = maxDistance;
        }

        Vector3 pos = GroundHelper.localPosition;
        pos.y = -scale.y / 2f;
        GroundHelper.localPosition = pos;

        GroundHelper.localScale = scale;
    }

}
