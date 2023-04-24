﻿using System.Collections.Generic;
using UnityEngine;

public partial class KeyBinds : MonoBehaviour, IKeyBinds
{
    public static KeyBinds Instance;

    [Header("Mouse")]
    [Range(0f, 1000f)]
    public float mouseSensitivity = 100f;
    public bool mouseInversion = false;

    [Header("Actions")]
    public KeyCode shoot = KeyCode.Mouse0;
    public KeyCode aim = KeyCode.Mouse1;
    public KeyCode interact = KeyCode.E;

    [Header("Movement")]
    public KeyCode moveRight = KeyCode.D;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveForward = KeyCode.W;
    public KeyCode moveBackward = KeyCode.S;
    public KeyCode jump = KeyCode.Space;
    public KeyCode dash = KeyCode.LeftShift;

    public float MouseSensitivity => mouseSensitivity;
    public float MouseInversion => mouseInversion ? 1f : -1f;

    public KeyCode Shoot => keyActionPairs[KeyActions.Shoot];
    public KeyCode Aim => keyActionPairs[KeyActions.Aim];

    public KeyCode MoveRight => keyActionPairs[KeyActions.Right];
    public KeyCode MoveLeft => keyActionPairs[KeyActions.Left];
    public KeyCode MoveForward => keyActionPairs[KeyActions.Forward];
    public KeyCode MoveBackward => keyActionPairs[KeyActions.Backward];
    public KeyCode Jump => keyActionPairs[KeyActions.Jump];
    public KeyCode Dash => keyActionPairs[KeyActions.Dash];
    public KeyCode Interact => keyActionPairs[KeyActions.Interact];

    public Dictionary<KeyActions, KeyCode> keyActionPairs { get; private set; } = new Dictionary<KeyActions, KeyCode>();

    private void Awake()
    {
        Instance = this;

        keyActionPairs = new Dictionary<KeyActions, KeyCode>() 
        {
            { KeyActions.Right, moveRight },
            { KeyActions.Left, moveLeft },
            { KeyActions.Forward, moveForward },
            { KeyActions.Backward, moveBackward },

            { KeyActions.Jump, jump },
            { KeyActions.Dash, dash },

            { KeyActions.Shoot, shoot },
            { KeyActions.Aim, aim },
            { KeyActions.Interact, interact }
        };
    }

    public void Bind(KeyActions action, KeyCode key) => keyActionPairs[action] = key;

    public KeyCode GetBind(KeyActions action) => keyActionPairs[action];
}

