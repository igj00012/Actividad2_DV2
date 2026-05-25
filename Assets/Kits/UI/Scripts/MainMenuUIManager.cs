using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject mainMenu;

    [SerializeField] AudioClip mouseSFX;
    [SerializeField] AudioClip clickSFX;

    AudioSource source;
    SettingsManager settings;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        settings = GetComponent<SettingsManager>();
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
    }

    private void CloseSettings()
    {
        settingsCanvas.SetActive(false);
        mainMenu.SetActive(true);
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
