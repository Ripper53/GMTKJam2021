using UnityEngine;
using AI;
using System.Collections;

public class ArtificialIntelligenceAudio : AudioObject {
    public ArtificialIntelligence ArtificialIntelligence;
    public float MinTime, MaxTime;
    public AudioClip[] AudioClips;

    private Coroutine coroutine;

    protected void OnEnable() {
        coroutine = StartCoroutine(PlayRandomClipAfterTime());
    }

    private IEnumerator PlayRandomClipAfterTime() {
        yield return new WaitForSeconds(Random.Range(MinTime, MaxTime));

        PlayAudio(AudioClips[Random.Range(0, AudioClips.Length)]);

        coroutine = StartCoroutine(PlayRandomClipAfterTime());
    }

    protected void OnDisable() {
        StopCoroutine(coroutine);
    }

    public void Play(AudioClip audioClip) {
        StopCoroutine(coroutine);
        coroutine = StartCoroutine(PlayRandomClipAfterTime());
        PlayAudio(audioClip);
    }

}
