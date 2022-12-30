using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerKeybinds : MonoBehaviour
{
    [SerializeField] public KeyCode moveRight = KeyCode.D;
    [SerializeField] public KeyCode moveLeft = KeyCode.A;
    [SerializeField] public KeyCode moveForwad = KeyCode.W;
    [SerializeField] public KeyCode moveBackward = KeyCode.S;

    [SerializeField] public KeyCode jump = KeyCode.Space;
}
