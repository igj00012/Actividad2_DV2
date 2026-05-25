using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI batteryLeft;
    [SerializeField] Image healthBar;
    [SerializeField] TextMeshProUGUI currentObjective;
    [SerializeField] GameObject gameplayMenu;
    [SerializeField] GameObject victoryMenu;
    [SerializeField] GameObject defeatMenu;
    [SerializeField] TextMeshProUGUI tutorial;
    [SerializeField] InputManagerSO inputManager;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pauseContent;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] AudioSource gameSource;

    [Header("Parameters")]
    [SerializeField] float delayTutorialTimer = 5;

    [Header("SFX")]
    [SerializeField] AudioClip mouseSFX;
    [SerializeField] AudioClip clickSFX;
    [SerializeField] AudioClip winSFX;
    [SerializeField] AudioClip failSFX;
    [SerializeField] AudioClip noiseSFX;

    AudioSource source;
    SettingsManager settings;

    public static GameplayUIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        victoryMenu.SetActive(false);
        defeatMenu.SetActive(false);
        pauseMenu.SetActive(false);
        tutorial.enabled = false;

        source = GetComponent<AudioSource>();
        settings = GetComponent<SettingsManager>();
    }

    private void OnEnable()
    {
        inputManager.OnPause += OpenPause;

        settings.OnSettingsSaved += CloseSettings;
    }

    private void OnDisable()
    {
        inputManager.OnPause -= OpenPause;

        settings.OnSettingsSaved -= CloseSettings;
    }

    public void SetBatteryVisibility(bool visible)
    {
        batteryLeft.gameObject.SetActive(visible);
    }

    public void ChangeBattery(int battery)
    {
        batteryLeft.SetText(battery + " %");
    }

    public void ChangeHP(float newHP)
    {
        healthBar.fillAmount = newHP;
    }

    public void ChangeObjective(string text)
    {
        currentObjective.SetText(text);
    }

    public void Defeat()
    {
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;

        gameSource.PlayOneShot(winSFX);

        source.loop = true;
        source.PlayOneShot(noiseSFX);

        gameplayMenu.SetActive(false);
        defeatMenu.SetActive(true);
    }

    public void Victory()
    {
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;

        gameSource.PlayOneShot(winSFX);

        gameplayMenu.SetActive(false);
        victoryMenu.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;

        source.PlayOneShot(clickSFX);

        SceneManager.LoadScene("Level1");
    }

    public void Quit()
    {
        Time.timeScale = 1;

        source.PlayOneShot(clickSFX);

        SceneManager.LoadScene("MainMenu");
    }

    public void ShowTutorial(string newTutorial)
    {
        tutorial.enabled = true;
        tutorial.SetText(newTutorial);
        StartCoroutine(TutorialTimer());
    }

    IEnumerator TutorialTimer()
    {
        yield return new WaitForSeconds(delayTutorialTimer);
        tutorial.enabled = false;
    }

    void OpenPause()
    {
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;

        gameplayMenu.SetActive(false);
        pauseMenu.SetActive(true);
        pauseContent.SetActive(true);
    }

    public void Resume()
    {
        source.PlayOneShot(clickSFX);

        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;

        gameplayMenu.SetActive(true);
        pauseMenu.SetActive(false);
        pauseContent.SetActive(false);
    }

    public void OpenSettings()
    {
        source.PlayOneShot(clickSFX);

        settingsMenu.SetActive(true);
        pauseContent.SetActive(false);
    }

    private void CloseSettings()
    {
        settingsMenu.SetActive(false);
        pauseContent.SetActive(true);
    }

    public void MouseEnter()
    {
        source.PlayOneShot(mouseSFX);
    }
}
