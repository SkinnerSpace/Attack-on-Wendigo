using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviour
{
    private PlayerCharacter player;
    private CharacterController controller;
    private IKeyBinds keys;

    private float xAxis;
    private float zAxis;
    private Vector3 direction;

    private void Awake()
    {
        player = GetComponent<PlayerCharacter>();
        controller = GetComponent<CharacterController>();
        keys = GetComponent<IKeyBinds>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void HandleMovement()
    {
        float moveRight = Input.GetKey(keys.MoveRight) ? 1f : 0f;
        float moveLeft = Input.GetKey(keys.MoveLeft) ? -1f : 0f;
        xAxis = moveRight + moveLeft;

        float moveForward = Input.GetKey(keys.MoveForward) ? 1f : 0f;
        float moveBackward = Input.GetKey(keys.MoveBackward) ? -1f : 0f;
        zAxis = moveForward + moveBackward;

        Vector3 directionRaw = new Vector3(xAxis, 0f, zAxis).normalized;
        direction = (directionRaw.x * transform.right) + (directionRaw.z * transform.forward);
    }

    private void ApplyMovement()
    {
        controller.Move(direction * player.Speed * Time.deltaTime);
    }
}
