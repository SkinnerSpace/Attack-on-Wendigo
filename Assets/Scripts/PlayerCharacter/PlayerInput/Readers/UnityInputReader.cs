using UnityEngine;

public class UnityInputReader : MonoBehaviour
{
    public bool Pressed(KeyCode key) => Input.GetKeyDown(key);
    public bool Hold(KeyCode key) => Input.GetKey(key);
    public bool Released(KeyCode key) => Input.GetKeyUp(key);
    public Vector2 MousePos => new Vector2(-Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
}


