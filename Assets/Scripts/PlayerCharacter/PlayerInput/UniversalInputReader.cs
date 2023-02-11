using UnityEngine;

public class UniversalInputReader : MonoBehaviour
{
    public bool Pressed(KeyCode key) => Input.GetKeyDown(key);
    public bool Hold(KeyCode key) => Input.GetKey(key);
    public bool Released(KeyCode key) => Input.GetKeyUp(key);
}


