using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour {

    private bool loadingScene = false;

    protected void OnTriggerEnter(Collider other) {
        if (loadingScene || !other.CompareTag("Player")) return;
        loadingScene = true;
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        if (index < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(index);
    }

}
