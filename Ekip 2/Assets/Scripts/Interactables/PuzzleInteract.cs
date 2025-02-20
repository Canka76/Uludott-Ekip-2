using UnityEngine;

public abstract class PuzzleInteract : Interactable
{
    [Header("Puzzle Settings")]
    public GameObject player;
    public KeyCode closeKey = KeyCode.F;
    
    [Header("Camera Settings")]
    public Camera playerCamera;
    public Camera puzzleCamera;
    
    protected bool isPuzzleOpen = false;
    private bool isPuzzleComplete = false;
    private FpsController fpsController;

    private void Awake()
    {
        fpsController = player.GetComponent<FpsController>();
        // Puzzle kamera başlangıçta devre dışı, player kamera aktif.
        puzzleCamera.enabled = false;
        playerCamera.enabled = true;
    }
    
    public override void OnFocus() { }

    public override void OnLoseFocus() { }

    public override void OnInteract()
    {
        Debug.Log("Puzzle Interacted");
        OnPuzzleInteract(); // Dönüş değeri kullanılmıyor; gerekiyorsa kontrol edilebilir.
        isPuzzleOpen = true;
        playerCamera.enabled = false;
        puzzleCamera.enabled = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        fpsController.enabled = false;
    }

    private void Update()
    {
        if (!isPuzzleOpen) return;
        
        OnPuzzleUpdate();
        
        if (OnPuzzleComplete(isPuzzleComplete))
        {
            ClosePuzzle();
        }
    }
    
    public void ClosePuzzle()
    {
        OnPuzzleClose();
        isPuzzleOpen = false;
        playerCamera.enabled = true;
        puzzleCamera.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        fpsController.enabled = true;
    }

    public void OnPuzzleUpdate()
    {
        if (Input.GetKeyDown(closeKey))
        {
            ClosePuzzle();
        }
    }
    
    public abstract bool OnPuzzleInteract();
    public abstract bool OnPuzzleComplete(bool isPuzzleComplete);
    public abstract bool OnPuzzleReset();
    public abstract bool OnPuzzleClose();
}