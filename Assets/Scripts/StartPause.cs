using System.Collections;
using UnityEngine;

public class StartPause : MonoBehaviour {
    public float Time = 3f;

    protected void Start() {
        UnityEngine.Time.timeScale = 0f;
        StartCoroutine(Begin());
    }

    private IEnumerator Begin() {
        yield return new WaitForSecondsRealtime(Time);
        UnityEngine.Time.timeScale = 1f;
    }

}
