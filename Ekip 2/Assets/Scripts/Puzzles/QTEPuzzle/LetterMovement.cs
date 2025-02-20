using UnityEngine;

public class LetterMovement : MonoBehaviour
{
    public Transform targetTransform; // Harflerin gideceği yer
    public Transform sariDaire;
    public KeyCode key;
    public float speed = 0.001f;

    public delegate void OnClickInsideOfCircleDelegate();
    public event OnClickInsideOfCircleDelegate OnClickInsideOfCircle;
    
    public delegate void OnClickOutsideOfCircleDelegate();
    public event OnClickOutsideOfCircleDelegate OnClickOutsideOfCircle;
    
    
    private bool isPressed = false;

    void Update()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
        transform.position = newPosition;
        if (sariDaire != null)
        {
            float distance = Vector3.Distance(transform.position, sariDaire.position);
            if (distance < 0.05f)
            {
                if (Input.GetKeyDown(key))
                {
                    OnClickInsideOfCircle?.Invoke();
                    Destroy(gameObject);
                }
            }
            else if (targetTransform.position == transform.position)
            {
                OnClickOutsideOfCircle?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}