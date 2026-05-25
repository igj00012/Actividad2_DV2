using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsSelectedObject;
    [SerializeField] GameObject mainMenuSelectedObject;

    [Header("SFX")]
    [SerializeField] AudioClip mouseSFX;
    [SerializeField] AudioClip clickSFX;

    AudioSource source;
    SettingsManager settings;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        settings = GetComponent<SettingsManager>();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuSelectedObject);
    }

    private void OnEnable()
    {
        settings.OnSettingsSaved += CloseSettings;
    }

    private void OnDisable()
    {
        settings.OnSettingsSaved -= CloseSettings;
    }

    public void Play()
    {
        source.PlayOneShot(clickSFX);

        SceneManager.LoadScene("Level1");
    }

    public void OpenSettings()
    {
        source.PlayOneShot(clickSFX);

        settingsCanvas.SetActive(true);
        mainMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsSelectedObject);
    }

    private void CloseSettings()
    {
        settingsCanvas.SetActive(false);
        mainMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuSelectedObject);
    }

    public void Quit()
    {
        source.PlayOneShot(clickSFX);

        Application.Quit();
    }

    public void MouseEnter()
    {
        source.PlayOneShot(mouseSFX);
    }
}
