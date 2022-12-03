using UnityEngine;

public class Momentum : MonoBehaviour
{
    private Character character;

    private void Awake()
    {
        character = GetComponent<Movement>().Character;
    }

    public Vector3 ApplyTo(Vector3 velocity)
    {
        Vector3 modifiedVelocity = velocity;
        modifiedVelocity += character.data.velocityMomentum;
        return modifiedVelocity;
    }

    public void Decelerate()
    {
        if (character.data.velocityMomentum.magnitude >= 0f)
        {
            character.data.velocityMomentum -= (character.data.velocityMomentum * character.data.momentumDeceleration) * Time.deltaTime;
            StopMomentum();
        }
    }

    private void StopMomentum()
    {
        if (character.data.velocityMomentum.magnitude < 0f)
            character.data.velocityMomentum = Vector3.zero;
    }
}
