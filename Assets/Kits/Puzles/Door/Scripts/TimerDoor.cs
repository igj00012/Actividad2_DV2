using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class TimerDoor : PuzzleButtonBase
{
    [Header("Parameters")]
    [SerializeField] float timer = 10f;

    [Header("SFX")]
    [SerializeField] AudioClip closedDoor;

    bool firstButton = true;
    Coroutine timeCoroutine;
    protected override void CheckButtonsPressed()
    {
        // El timer empieza cuando se pulsa el primer botón
        if (firstButton)
        {
            firstButton = false;
            timeCoroutine = StartCoroutine(ButtonTimer());
        }

        if (pressedButtons.Count == requiredButtons.Length)
        {
            if (timeCoroutine != null) {
                StopCoroutine(ButtonTimer());
                SolvedPuzzle();
            }
        }
    }

    IEnumerator ButtonTimer()
    {
        float t = 0;

        while (t < timer)
        {
            t += Time.deltaTime;

            Debug.Log(t % 10);

            yield return null;
        }

        ResetButtons();
    }

    bool solvedPuzzle = false;
    protected override void SolvedPuzzle()
    {
        base.SolvedPuzzle();

        solvedPuzzle = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (solvedPuzzle)
            {
                // Abrir puerta, victoria
                Debug.Log("Has ganado");
            }
            else
            {
                source.PlayOneShot(closedDoor);
            }
        }
    }
}
