using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerKeyBinds : MonoBehaviour, IKeyBinds
{
    [Header("Mouse")]
    [Range(0f, 1000f)]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private bool mouseInversion = false;

    public float MouseSensitivity => mouseSensitivity;
    public float MouseInversion => mouseInversion ? 1f : -1f;
    
    [Header("Actions")]
    [SerializeField] private KeyCode shoot = KeyCode.Mouse0;
    [SerializeField] private KeyCode aim = KeyCode.Mouse1;
    [SerializeField] private KeyCode reload = KeyCode.R;

    public KeyCode Shoot => shoot;
    public KeyCode Aim => aim;
    public KeyCode Reload => reload;

    [Header("Movement")]
    [SerializeField] private KeyCode moveRight = KeyCode.D;
    [SerializeField] private KeyCode moveLeft = KeyCode.A;
    [SerializeField] private KeyCode moveForward = KeyCode.W;
    [SerializeField] private KeyCode moveBackward = KeyCode.S;
    [SerializeField] private KeyCode jump = KeyCode.Space;

    public KeyCode MoveRight => moveRight;
    public KeyCode MoveLeft => moveLeft;
    public KeyCode MoveForward => moveForward;
    public KeyCode MoveBackward => moveBackward;
    public KeyCode Jump => jump;
    
}
