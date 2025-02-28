using System.Collections;
using UnityEngine;

public class RotateOnXAxisOnly : IClicked
{
    public float xAngle = 0f;
    public float rotationSpeed = 50f;
    public bool canTurn = false;
    public bool isPuzzleSolved = false;

    [SerializeField] private float delay = 1f;
    [SerializeField] private EnvPuzzleManager puzzleManager;
    [SerializeField] private float correctRotationMin = -135f;
    [SerializeField] private float correctRotationMax = -75f;
    [SerializeField] private float targetRotation = -90f;

    private bool canCheck = true;
    private Vector3 initialRotation;

    void Start()
    {
        initialRotation = transform.eulerAngles;
    }

    void Update()
    {
        Rotate();

        if (canTurn && Input.GetKeyDown(KeyCode.E))
        {
            Clicked();
        }
    }

    void Rotate()
    {
        if (canTurn && !isPuzzleSolved)
        {
            xAngle += rotationSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(xAngle, 90f, -90f);
        }
    }

    public override void Clicked()
    {
        if (canCheck)
        {
            StartCoroutine(CheckPass());
        }
    }

    IEnumerator CheckPass()
    {
        float xRotation = NormalizeAngle(transform.eulerAngles.x);

        if (xRotation <= correctRotationMax && xRotation >= correctRotationMin)
        {
            Debug.Log("✅ Correct rotation!");
            transform.rotation = Quaternion.Euler(targetRotation, 90f, -90f);
            isPuzzleSolved = true;
            canCheck = false; // Prevent further checks

            if (puzzleManager != null)
            {
                puzzleManager.PuzzleCompleted(this);
            }
        }
        else
        {
            Debug.Log("❌ Incorrect rotation. Restarting...");
            canCheck = false;
            yield return new WaitForSeconds(delay);

            if (puzzleManager != null)
            {
                puzzleManager.RestartPuzzles();
            }
        }
    }

    public void ResetPuzzle()
    {
        xAngle = 0f;
        isPuzzleSolved = false;
        canTurn = false;
        canCheck = true; // Reset validation flag
        transform.eulerAngles = initialRotation;
        Debug.Log($"{gameObject.name} reset. canTurn: {canTurn}, canCheck: {canCheck}");
    }

    public void EnableChecking(bool enable)
    {
        canCheck = enable; // Allow external control
    }

    float NormalizeAngle(float angle)
    {
        angle %= 360f;
        return (angle > 180f) ? angle - 360f : angle;
    }
}