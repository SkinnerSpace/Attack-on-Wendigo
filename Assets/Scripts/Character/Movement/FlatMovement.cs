using UnityEngine;

public class FlatMovement : MonoBehaviour, ICharacterDependee
{
    private Character character;

    public void SetUp(Character character)
    {
        this.character = character;
    }

    public Vector3 Calculate(float speed)
    {
        Vector3 rawDirection = character.controller.GetRawDirection();
        Vector3 velocity = (transform.right * rawDirection.x) + (transform.forward * rawDirection.z);
        velocity = velocity.normalized * speed;

        return velocity;
    }
}
