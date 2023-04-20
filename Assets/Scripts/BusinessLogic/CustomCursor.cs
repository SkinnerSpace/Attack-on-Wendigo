using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private CanvasGroup image;

    public static CustomCursor Instance;

    private void Awake()
    {
        Instance = this;
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void Unlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        image.alpha = 1f;
    }

    public void Lock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        image.alpha = 0f;
    }
}