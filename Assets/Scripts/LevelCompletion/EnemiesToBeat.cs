using UnityEngine;
using AI;

public class EnemiesToBeat : MonoBehaviour {
    public GameObject Flag;
    public Wave[] Waves;

    [System.Serializable]
    public class Wave {
        public ArtificialIntelligence[] Enemies;
    }

    private int waveIndex = 0;

    protected void Awake() {
        SetupWave(Waves[0]);
    }

    private void ProceedWithNextWave() {
        waveIndex += 1;
        if (waveIndex < Waves.Length)
            SetupWave(Waves[waveIndex]);
        else
            Flag.SetActive(true);
    }

    private void SetupWave(Wave wave) {
        int killedCount = 0;
        foreach (ArtificialIntelligence ai in wave.Enemies) {
            ai.Killed += source => {
                killedCount += 1;
                if (wave.Enemies.Length == killedCount)
                    ProceedWithNextWave();
            };
            ai.gameObject.SetActive(true);
        }
    }

}
