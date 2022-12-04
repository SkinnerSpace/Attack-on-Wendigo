using UnityEngine;

public class Walk : MonoBehaviour, ICommand
{
    [SerializeField] private float speed = 10f;
    private CharacterController body;

    private void Awake()
    {
        body = GetComponent<CharacterController>();
    }

    public void Execute()
    {
        Vector3 leftRight = transform.right * Input.GetAxisRaw("Horizontal");
        Vector3 backwardForward = transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 velocity = (leftRight + backwardForward).normalized * speed;

        body.Move(velocity * Time.deltaTime);
    }
}
