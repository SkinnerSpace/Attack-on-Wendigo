using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Momentum : MonoBehaviour
{
    private CharacterData data;

    private void Awake()
    {
        data = GetComponent<CharacterData>();
    }

    public Vector3 ApplyTo(Vector3 velocity)
    {
        Vector3 modifiedVelocity = velocity;
        modifiedVelocity += data.velocityMomentum;
        return modifiedVelocity;
    }

    public void Decelerate()
    {
        if (data.velocityMomentum.magnitude >= 0f)
        {
            data.velocityMomentum -= (data.velocityMomentum * data.momentumDeceleration) * Time.deltaTime;
            StopMomentum();
        }
    }

    private void StopMomentum()
    {
        if (data.velocityMomentum.magnitude < 0f)
            data.velocityMomentum = Vector3.zero;
    }
}
