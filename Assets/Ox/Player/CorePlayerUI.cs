using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using AI;

public class CorePlayerUI : MonoBehaviour {
    public Health Health;
    public CorePlayer CorePlayer;
    public CoreBall CoreBall;

    public Image ChargeBar;
    public GameObject HaveBall;
    public TargetAlphaUIAnimation HurtAlpha, HitAlpha;
    public Image RightHandImage, LeftHandImage;

    [Header("Throw Animation")]
    public Sprite HoldSprite;
    public Sprite ThrowingSprite;
    public Sprite DetachSprite;

    protected void Awake() {
        Health.Damaged += Health_Damaged;

        CorePlayer.ChangedChargeDelta += CorePlayer_ChangedChargeDelta;
        CorePlayer.Throwed += CorePlayer_Throwed;
        CorePlayer.Dash += CorePlayer_Dash;
        CorePlayer.Killed += CorePlayer_Killed;

        CoreBall.Grabbed += CoreBall_Grabbed;
        CoreBall.Deployed += CoreBall_Deployed;
    }

    private void Health_Damaged(Health source, int damage) {
        SetAlpha(HurtAlpha);
    }

    private void CorePlayer_ChangedChargeDelta(CorePlayer source, float chargeDelta) {
        float v = chargeDelta / source.ChargeMax;
        ChargeBar.fillAmount = v;
        if (v > 0f) {
            LeftHandImage.enabled = true;
            RightHandImage.sprite = ThrowingSprite;
        }
    }
    private void CorePlayer_Throwed(CorePlayer source) {
        LeftHandImage.enabled = false;
        RightHandImage.sprite = DetachSprite;
    }
    private void CorePlayer_Dash(CorePlayer source) {
        HaveBall.SetActive(true);
    }
    private void CorePlayer_Killed(CorePlayer source, ArtificialIntelligence ai) {
        SetAlpha(HitAlpha);
    }

    private void CoreBall_Grabbed(CoreBall source) {
        RightHandImage.sprite = HoldSprite;
        HaveBall.SetActive(true);
    }

    private void CoreBall_Deployed(CoreBall source) {
        HaveBall.SetActive(false);
    }

    #region Alpha Animation
    private void SetAlpha(TargetAlphaUIAnimation anim) {
        anim.SetTargetAlpha(1f);
        StartCoroutine(SetAlphaTime(anim));
    }
    private IEnumerator SetAlphaTime(TargetAlphaUIAnimation anim) {
        yield return new WaitForSecondsRealtime(anim.Time);
        anim.SetTargetAlpha(0f);
    }
    #endregion

}
