using UnityEngine;
using UnityEngine.UI;

public class TargetAlphaUIAnimation : MonoBehaviour {
    public Image Image;
    public float Time;

    private float targetAlpha = 0f;
    public void SetTargetAlpha(float alpha) {
        targetAlpha = alpha;
    }

    private float alphaVel = 0f;

    protected void Update() {
        Color color = Image.color;
        color.a = Mathf.SmoothDamp(color.a, targetAlpha, ref alphaVel, Time, Mathf.Infinity, UnityEngine.Time.unscaledDeltaTime);
        Image.color = color;
    }

}
