using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour {
    public AudioClip AudioClip;
    public AudioClip Music;

    private bool loadingScene = false;

    protected void OnTriggerEnter(Collider other) {
        if (loadingScene || !other.CompareTag("Player")) return;
        loadingScene = true;
        AudioControls.Singleton.PlaySFX(AudioClip);
        AudioControls.Singleton.PlayMusic(Music);
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        if (index < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(index);
        else
            SceneManager.LoadScene(0);
    }

}
