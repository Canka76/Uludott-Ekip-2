using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public RotateOnXAxisOnly[] puzzles;
    private int currentPuzzleIndex = 0;

    [SerializeField] float nextPuzzleDelay = 0.5f;
    void Start()
    {
        EnablePuzzle(currentPuzzleIndex);
    }

    public void PuzzleCompleted(RotateOnXAxisOnly puzzle)
    {
        int index = System.Array.IndexOf(puzzles, puzzle);

        if (index == currentPuzzleIndex)
        {
            Debug.Log($"✅ Puzzle {index + 1} completed!");
            currentPuzzleIndex++;

            if (currentPuzzleIndex < puzzles.Length)
            {
                StartCoroutine(ActivateNextPuzzleAfterDelay(nextPuzzleDelay)); // Add delay
            }
            else
            {
                Debug.Log("🎉 All puzzles solved!");
            }
        }
    }

    private IEnumerator ActivateNextPuzzleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait before activating next puzzle
        EnablePuzzle(currentPuzzleIndex);
    }

    public void RestartPuzzles()
    {
        Debug.Log("🔄 Restarting puzzles...");
        currentPuzzleIndex = 0;
        foreach (var puzzle in puzzles) puzzle.ResetPuzzle();
        EnablePuzzle(currentPuzzleIndex);
    }

    private void EnablePuzzle(int index)
    {
        for (int i = 0; i < puzzles.Length; i++)
        {
            if (i == index)
            {
                puzzles[i].ResetPuzzle(); // Reset the puzzle state
                puzzles[i].canTurn = true;
                puzzles[i].EnableChecking(true);
                Debug.Log($"🔓 Puzzle {index + 1} is now active. canTurn: {puzzles[i].canTurn}, canCheck: ");
            }
            else
            {
                puzzles[i].canTurn = false;
                puzzles[i].EnableChecking(false);
            }
        }
    }
}