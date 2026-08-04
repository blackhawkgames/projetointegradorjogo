using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSettingsMenu : MonoBehaviour
{
    [Header("Componentes de UI")]
    public TMP_Dropdown graphicsDropdown;
    public Slider volumeSlider;
    public Slider sensitivitySlider;

    private void Start()
    {
        UpdateUIValues();

        graphicsDropdown.onValueChanged.AddListener(SetQuality);
        volumeSlider.onValueChanged.AddListener(SetVolume);
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
    }

    private void UpdateUIValues()
    {
        if (GameManager.Instance == null) return;

        graphicsDropdown.value = GameManager.Instance.qualidadeGrafica;
        volumeSlider.value = GameManager.Instance.volumeMaster;
        sensitivitySlider.value = GameManager.Instance.sensibilidadeMouse;
    }

    public void SetQuality(int qualityIndex)
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.qualidadeGrafica = qualityIndex;
        GameManager.Instance.ApplySettings();
        GameManager.Instance.SaveGame();
    }

    public void SetVolume(float volume)
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.volumeMaster = volume;
        GameManager.Instance.ApplySettings();
        GameManager.Instance.SaveGame();
    }

    public void SetSensitivity(float sensitivity)
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.sensibilidadeMouse = sensitivity;
        GameManager.Instance.ApplySettings();
        GameManager.Instance.SaveGame();
    }
}