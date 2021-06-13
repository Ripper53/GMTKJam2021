using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePlayerAudio : AudioObject {
    public CorePlayer CorePlayer;
    public AudioClip ThrowAudioClip, DashAudioClip;
    public AudioClip[] DeathAudioClips;

    protected void Awake() {
        CorePlayer.Throwed += CorePlayer_Throwed;
        CorePlayer.Dash += CorePlayer_Dash;
        CorePlayer.Killed += CorePlayer_Killed;
    }

    private void CorePlayer_Throwed(CorePlayer source) {
        PlayAudio(ThrowAudioClip);
    }

    private void CorePlayer_Dash(CorePlayer source) {
        PlayAudio(DashAudioClip);
    }

    private void CorePlayer_Killed(CorePlayer source, AI.ArtificialIntelligence ai) {
        PlayAudio(DeathAudioClips[Random.Range(0, DeathAudioClips.Length)]);
    }

}
