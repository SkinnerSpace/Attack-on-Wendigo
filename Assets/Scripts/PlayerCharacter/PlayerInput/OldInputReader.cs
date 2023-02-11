using System.Collections.Generic;
using UnityEngine;

public class OldInputReader : MonoBehaviour
{
    public static OldInputReader Instance { get; private set; }

    private static DirectionReader directionReader;
    public static bool right => directionReader.right;
    public static bool left => directionReader.left;
    public static bool forward => directionReader.forward;
    public static bool backward => directionReader.backward;
    public static Vector3 rawDir => directionReader.rawDir;
    public static Vector3 normDir => directionReader.normDir;

    
    private static MouseReader mouseReader;
    public static Vector2 mouse => mouseReader.mouse;
    public static bool shoot => mouseReader.shoot;
    public static bool aim => mouseReader.aim;

    public bool Attack => mouseReader.shoot;
    public bool Aim => mouseReader.aim;

    public static bool jump { get; private set; }
    public static bool dash { get; private set; }
    public static bool interact { get; private set; }

    public IKeyBinds keys { get; private set; }

    private OldInputReader() { }

    private void Awake()
    {
        PreserveSingleton();
        keys = GetComponent<IKeyBinds>();

        directionReader = new DirectionReader(keys);
        mouseReader = new MouseReader();
    }

    private void PreserveSingleton()
    {
        if (Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
        }
    }

    private void Update() => ReadInput();

    private void ReadInput()
    {
        directionReader.Read();
        mouseReader.Read();
        ReadJumpInput();
        ReadDashInput();
        ReadInteractInput();
    }

    private void ReadJumpInput() => jump = Input.GetKey(keys.Jump);
    private void ReadDashInput() => dash = Input.GetKeyDown(keys.Dash);
    private void ReadInteractInput() => interact = Input.GetKeyDown(keys.Interact);
}
