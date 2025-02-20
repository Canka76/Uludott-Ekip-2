using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class QTEPuzzleManager : MonoBehaviour
{
    public static QTEPuzzleManager instance;
    
    [Header("Canvas")]
    public Transform canvasTransform;
    [Header("Prefabs")]
    public GameObject letterPrefab;
    public Transform spawnPoint; 
    public Transform targetTransform;
    public Transform sariDaire;

    [Header("Process Text")]
    public TMPro.TextMeshProUGUI processText;
    
    [Header("Spawn Settings")]
    public float spawnInterval = 100f ;
    public float letterSpeed = 200f;
    [Header("Process Settings")]
    public float decreaseRate = 1f;
    public float increaseRate = 5f;
    
    private float process = 0;
    
    private List<KeyCode> keyCodes = new List<KeyCode>
    {
        KeyCode.L, KeyCode.O, KeyCode.V, KeyCode.R, KeyCode.T, KeyCode.E,
    };

    private int randIndex = -1;

    private float timer;
    private bool isFocused = false;

    void Awake()
    {
        instance = this;
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= (spawnInterval) && isFocused)
        {
            randIndex = Random.Range(0, keyCodes.Count);
            SpawnLetter();
            timer = 0f;
        }
    }

    void SpawnLetter()
    {
        GameObject go = Instantiate(letterPrefab, spawnPoint.position, Quaternion.Euler(0, -90, 0), canvasTransform);
        
        LetterMovement letterMovement = go.GetComponent<LetterMovement>();
        if (letterMovement != null)
        {
            var keyCode = keyCodes[randIndex];
            letterMovement.targetTransform = targetTransform;
            letterMovement.sariDaire = sariDaire;
            letterMovement.key = keyCode;
            letterMovement.speed = letterSpeed;

            letterMovement.OnClickInsideOfCircle += () => ProcessAdd(increaseRate);
            letterMovement.OnClickOutsideOfCircle += () => ProcessSub(decreaseRate);
            
        }
        
        TMPro.TextMeshPro textComponent = go.GetComponent<TMPro.TextMeshPro>();
        if (textComponent != null)
        {
            textComponent.text = keyCodes[randIndex].ToString();
        }
    }
    
    void ProcessAdd(float value)
    {
        process = Mathf.Clamp(process + value, 0, 100);
        processText.text = "Process: " + process.ToString();
    }
    
    void ProcessSub(float value)
    {
        process = Mathf.Clamp(process - value, 0, 100);
        processText.text = "Process: " + process.ToString();
    }
    
    public void IsFocusState(bool state)
    {
        if (state)
        {
            processText.gameObject.SetActive(true);
        }
        else
        {
            processText.gameObject.SetActive(false);
        }
        isFocused = state;
    }

    public float getProcess()
    {
        return process;
    }
}
