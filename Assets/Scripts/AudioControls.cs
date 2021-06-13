using System.Collections;
using UnityEngine;

public class AudioControls : MonoBehaviour {
    public static AudioControls Singleton { get; private set; } = null;

    public AudioSource MusicSource, SFXSource;
    public float VolumeFadeInSpeed, VolumeFadeOutSpeed;

    private Coroutine coroutine;
    protected void Awake() {
        if (Singleton) {
            Destroy(gameObject);
        } else {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
            coroutine = StartCoroutine(Up());
        }
    }

    public void PlaySFX(AudioClip audioClip) {
        SFXSource.Stop();
        SFXSource.clip = audioClip;
        SFXSource.Play();
    }

    public void PlayMusic(AudioClip audioClip) {
        StopCoroutine(coroutine);
        coroutine = StartCoroutine(Down(audioClip));
    }

    private IEnumerator Up() {
        float s = 0f;
        while (s < 1f) {
            yield return new WaitForEndOfFrame();
            s += VolumeFadeInSpeed * Time.unscaledDeltaTime;
            MusicSource.volume = s;
        }
    }

    private IEnumerator Down(AudioClip audioClip) {
        float s = MusicSource.volume;
        while (s > 0f) {
            yield return new WaitForEndOfFrame();
            s -= VolumeFadeOutSpeed * Time.unscaledDeltaTime;
            MusicSource.volume = s;
        }
        MusicSource.Stop();
        MusicSource.clip = audioClip;
        MusicSource.Play();
        coroutine = StartCoroutine(Up());
    }

}
