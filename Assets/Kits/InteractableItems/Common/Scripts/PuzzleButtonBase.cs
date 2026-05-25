using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButtonBase : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] protected Button[] requiredButtons;
    protected List<int> pressedButtons = new List<int>();

    [Header("SFX")]
    [SerializeField] AudioClip incorrectSFX;
    [SerializeField] protected AudioClip correctSFX;

    protected AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        foreach (Button button in requiredButtons)
        {
            button.OnPressed += ButtonPressed;
        }
    }

    private void OnDisable()
    {
        foreach (Button button in requiredButtons)
        {
            button.OnPressed -= ButtonPressed;
        }
    }

    void ButtonPressed(int buttonId)
    {
        Debug.Log("Botón " + buttonId + " pulsado");

        if (pressedButtons.Contains(buttonId))
        {
            return;
        }

        pressedButtons.Add(buttonId);

        CheckButtonsPressed();
    }

    protected virtual void CheckButtonsPressed() { }

    protected virtual void SolvedPuzzle()
    {
        source.PlayOneShot(correctSFX);

        StartCoroutine(SFXDelay());
    }

    float offset = 4f;
    IEnumerator SFXDelay()
    {
        yield return new WaitForSeconds(correctSFX.length + offset);
    }

    protected void ResetButtons()
    {
        source.PlayOneShot(incorrectSFX);

        pressedButtons.Clear();

        foreach (Button button in requiredButtons)
        {
            button.ResetButton();
        }
    }
}
