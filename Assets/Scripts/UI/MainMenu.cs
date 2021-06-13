using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public string PlaySceneName;

    public void Play() {
        SceneManager.LoadScene(PlaySceneName);
    }

}
