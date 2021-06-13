using UnityEngine;

public abstract class AudioObject : MonoBehaviour {
    public AudioSource AudioSource;

    protected void PlayAudio(AudioClip clip) {
        AudioSource.Stop();
        AudioSource.pitch = Random.Range(0.9f, 1.1f);
        AudioSource.clip = clip;
        AudioSource.Play();
    }

}
