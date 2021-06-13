using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour {
    public GameObject Menu;
    public CorePlayer CorePlayer;
    public CoreView CoreView;
    public AudioMixer AudioMixer;
    public Slider SFXVolumeSlider, MusicVolumeSlider;
    public TMP_InputField SensitivityInputField;

    public static float SFXVolume = 1f, MusicVolume = 1f;
    public static float Sensitivity = 1f;

    protected void Awake() {
        SFXVolumeSlider.SetValueWithoutNotify(SFXVolume);
        MusicVolumeSlider.SetValueWithoutNotify(MusicVolume);

        SensitivityInputField.SetTextWithoutNotify(Sensitivity.ToString());

        SFXVolumeSlider.onValueChanged.AddListener(v => {
            SFXVolume = AudioVolume(v);
            AudioMixer.SetFloat("SFXVolume", SFXVolume);
        });
        MusicVolumeSlider.onValueChanged.AddListener(v => {
            MusicVolume = AudioVolume(v);
            AudioMixer.SetFloat("MusicVolume", MusicVolume);
        });

        SensitivityInputField.onValueChanged.AddListener(v => {
            if (float.TryParse(v, out float result)) {
                Sensitivity = result;
                CoreView.aSensitivity = new Vector2(Sensitivity, Sensitivity);
            }
        });
    }

    public static float AudioVolume(float volume) {
        if (volume <= 0f) return -80f;
        return Mathf.Log10(volume) * 20f;
    }

    protected void Update() {
        if (Input.GetButtonDown("Cancel")) {
            if (Menu.activeSelf) {
                Cursor.lockState = CursorLockMode.Locked;
                Menu.SetActive(false);
                CorePlayer.StopInput = false;
            } else {
                Cursor.lockState = CursorLockMode.None;
                Menu.SetActive(true);
                CorePlayer.StopInput = true;
            }
        }
    }

}
