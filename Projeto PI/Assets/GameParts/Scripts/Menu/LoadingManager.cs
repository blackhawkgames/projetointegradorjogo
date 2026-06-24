using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;

    [System.Serializable]
    public class SceneLoadingData
    {
        public string sceneName;
        public string sceneDisplayName;
        public Sprite backgroundSprite;
    }

    [Header("Configurações das Cenas")]
    public List<SceneLoadingData> scenesConfig;
    public Sprite defaultBackground;

    [Header("Interface de Loading (Componentes de UI)")]
    public GameObject loadingPanel;
    public Slider progressBar;
    public TextMeshProUGUI sceneNameText;
    public Image backgroundImage;

    [Header("Configuração de Input")]
    public GameObject pressAnyKeyText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    private IEnumerator LoadAsynchronously(string sceneName)
    {
        SetupLoadingUI(sceneName);

        if (pressAnyKeyText != null)
        {
            pressAnyKeyText.SetActive(false);
        }

        if (loadingPanel != null)
        {
            loadingPanel.SetActive(true);
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        bool inputHooked = false;
        bool anyKeyPressed = false;

        Action<InputControl> inputAction = (control) => anyKeyPressed = true;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (progressBar != null)
            {
                progressBar.value = progress;
            }

            if (operation.progress >= 0.9f)
            {
                if (progressBar != null) progressBar.value = 1f;

                if (pressAnyKeyText != null)
                {
                    pressAnyKeyText.SetActive(true);
                }

                if (!inputHooked)
                {
                    inputHooked = true;
                    InputSystem.onAnyButtonPress.Call(inputAction);
                }

                if (anyKeyPressed)
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }

        if (loadingPanel != null)
        {
            loadingPanel.SetActive(false);
        }
    }

    private void SetupLoadingUI(string sceneName)
    {
        SceneLoadingData data = scenesConfig.Find(s => s.sceneName == sceneName);

        if (data != null)
        {
            if (sceneNameText != null) sceneNameText.text = data.sceneDisplayName;
            if (backgroundImage != null && data.backgroundSprite != null)
            {
                backgroundImage.sprite = data.backgroundSprite;
            }
        }
        else
        {
            if (sceneNameText != null) sceneNameText.text = sceneName;
            if (backgroundImage != null) backgroundImage.sprite = defaultBackground;
            Debug.LogWarning($"A cena '{sceneName}' não foi configurada na lista do LoadingManager!");
        }
    }
}