using UnityEngine;
using UnityEngine.UI;

public class CorePlayerUI : MonoBehaviour {
    public CorePlayer CorePlayer;
    public CoreBall CoreBall;

    public Image ChargeBar;
    public GameObject HaveBall;

    protected void Awake() {
        CorePlayer.ChangedChargeDelta += CorePlayer_ChangedChargeDelta;
        CorePlayer.Dash += CorePlayer_Dash;

        CoreBall.Grabbed += CoreBall_Grabbed;
        CoreBall.Deployed += CoreBall_Deployed;
    }

    private void CorePlayer_Dash(CorePlayer source) {
        HaveBall.SetActive(true);
    }
    private void CorePlayer_ChangedChargeDelta(CorePlayer source, float chargeDelta) {
        ChargeBar.fillAmount = chargeDelta / source.ChargeMax;
    }

    private void CoreBall_Grabbed(CoreBall source) {
        HaveBall.SetActive(true);
    }

    private void CoreBall_Deployed(CoreBall source) {
        HaveBall.SetActive(false);
    }

}
