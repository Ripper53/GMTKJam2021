using System.Collections;
using UnityEngine;

public class DeactivateAfterTime : MonoBehaviour {
    public GameObject GameObject;
    public float Time;

    protected Coroutine coroutine = null;
    protected void OnEnable() {
        coroutine = StartCoroutine(StartTimer());
    }

    protected void OnDisable() {
        StopCoroutine(coroutine);
    }

    private IEnumerator StartTimer() {
        yield return new WaitForSeconds(Time);
        GameObject.SetActive(false);
    }

}
