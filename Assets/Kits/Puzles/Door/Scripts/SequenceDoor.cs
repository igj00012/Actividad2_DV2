using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceDoor : PuzzleButtonBase
{
    protected override void CheckButtonsPressed()
    {
        // Cuando se han pulsado todos los botones se comprueba la secuencia
        if (pressedButtons.Count == requiredButtons.Length)
        {
            for (int i = 0; i < requiredButtons.Length; ++i)
            {
                // Si uno falla, secuencia incorrecta
                if (pressedButtons[i] != requiredButtons[i].GetId())
                {
                    ResetButtons();
                    return;
                }
            }

            SolvedPuzzle();
        }
    }

    protected override void SolvedPuzzle()
    {
        base.SolvedPuzzle();

        Destroy(gameObject);
    }
}
