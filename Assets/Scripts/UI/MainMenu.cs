using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public string PlaySceneName;

    public void Play() {
        SceneManager.LoadScene(PlaySceneName);
    }

    [SerializeField]
    private GameObject previousMenu;
    public void ActivateMenu(GameObject menu) {
        if (previousMenu)
            previousMenu.SetActive(false);
        menu.SetActive(true);
        previousMenu = menu;
    }

}
