using System.Collections;
using UnityEngine;
using TMPro;

public class StartPause : MonoBehaviour {
    public int Seconds = 3;
    public TextMeshProUGUI CountdownText;

    protected void Start() {
        Time.timeScale = 0f;
        StartCoroutine(Begin());
    }

    private IEnumerator Begin() {
        for (int i = 3; i > 0; i--) {
            CountdownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }
        CountdownText.enabled = false;
        Time.timeScale = 1f;
    }

}
