using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 20f;
    [SerializeField] public float jumpStrength = 30f;

    [NonSerialized] public Vector3 velocity;
    [NonSerialized] public Vector3 velocityMomentum;
    [NonSerialized] public float velocityY;
    [NonSerialized] public float momentumDeceleration = 3f;
    [NonSerialized] public float gravityForce = -60f; 
}