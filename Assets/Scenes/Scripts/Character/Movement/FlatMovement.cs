using UnityEngine;

public class FlatMovement : MonoBehaviour
{
    private Character character;

    private void Awake()
    {
        character = GetComponent<Movement>().Character;
    }

    public Vector3 Calculate(float speed)
    {
        Vector3 rawDirection = character.controller.GetRawDirection();
        Vector3 velocity = (transform.right * rawDirection.x) + (transform.forward * rawDirection.z);
        velocity = velocity.normalized * speed;

        return velocity;
    }
}
