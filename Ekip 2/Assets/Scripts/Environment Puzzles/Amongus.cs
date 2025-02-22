using System.Collections;
using UnityEngine;

public class RotateOnXAxisOnly : IClicked
{
    public float xAngle = 0f;            // Adjustable X-axis rotation
    public float rotationSpeed = 50f;    // Rotation speed for X-axis
    public bool canTurn = true;
    public bool passed1 = false;
    [SerializeField] private float delay = 1f;
    
    bool canCheck = true;
    
    void Update()
    {
        Rotate();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Clicked();
        }
    }

    void Rotate()
    {
        if (canTurn && !passed1)
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
        // Capture current X rotation before resetting
        float xRotation = NormalizeAngle(transform.eulerAngles.x);

        if (xRotation <= -75f && xRotation >= -135f)
        {
            Debug.Log("✅ X rotation is between -75° and -135°.");
            transform.rotation = Quaternion.Euler(-90f, 90f, -90f);
            passed1 = true;
        }
        else
        {
            Debug.Log("❌ X rotation is outside the range.");
            canCheck = false;
            yield return new WaitForSeconds(delay);
            canCheck = true;
          
        }
    }

    // Converts angles from [0°, 360°] to [-180°, 180°]
    float NormalizeAngle(float angle)
    {
        angle %= 360f;
        return (angle > 180f) ? angle - 360f : angle;
    }
}