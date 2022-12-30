using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private PlayerKeybinds keys;
    private PlayerData data;
    private Rigidbody body;

    private void Awake()
    {
        keys = GetComponent<PlayerKeybinds>();
        data = GetComponent<PlayerData>();
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        if (Input.GetKey(keys.jump) && data.isGrounded)
            Jump();
    }

    private void Jump()
    {
        body.AddForce(transform.up * data.jumpForce, ForceMode.Impulse);
    }
}
