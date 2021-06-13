using UnityEngine;

public class CoreBallAudio : AudioObject {
    public AudioClip[] CollisionAudioClips;

    protected void OnCollisionEnter(Collision collision) {
        PlayAudio(CollisionAudioClips[Random.Range(0, CollisionAudioClips.Length)]);
    }

}
