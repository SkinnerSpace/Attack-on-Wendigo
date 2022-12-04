using UnityEngine;

public class Jump : MonoBehaviour, ICommand
{
    [SerializeField] private float jumpStrength = 20f;
    private VerticalVelocityData data;

    private CharacterController body;
    private LayerMask groundLayer;

    private void Awake()
    {
        data = GetComponent<VerticalVelocityData>();
        groundLayer = 1 << 8;

        body = GetComponent<CharacterController>();
    }

    public void Execute()
    {
        if (IsGrounded())
            data.verticalVelocity = Input.GetKey(KeyCode.Space) ? jumpStrength : 0f;

        Vector3 jumpVelocity = new Vector3(0f, data.verticalVelocity, 0f);
        body.Move(jumpVelocity * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        return (Physics.CheckSphere(transform.position, 0.2f, groundLayer));
    }
}
