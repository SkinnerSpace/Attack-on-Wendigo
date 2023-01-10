using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform target;
    public Transform Target => target;

    [SerializeField] private Material orangeMaterial;
    [SerializeField] private Material blueMaterial;

    private CharacterController controller;
    private WaveMover waveMover;
    private AirMover airMover;
    
    [SerializeField] private MeshRenderer meshRenderer;

    private FunctionTimer timer;
    private float damageTime = 0.1f;

    private Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        airMover = GetComponent<AirMover>();
        waveMover = GetComponent<WaveMover>();

        timer = GetComponent<FunctionTimer>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        velocity = airMover.velocity + waveMover.velocity;
        controller.Move(velocity * Time.deltaTime);
    }

    public void ReceiveDamage(DamagePackage damagePackage)
    {
        meshRenderer.material = orangeMaterial;
        timer.Set("Color change", damageTime, ChangeColor);
        Debug.Log("Damage " + damagePackage.damage);
    }

    private void ChangeColor()
    {
        meshRenderer.material = blueMaterial;
    }
}

