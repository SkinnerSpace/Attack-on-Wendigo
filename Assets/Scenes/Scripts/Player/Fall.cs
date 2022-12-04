using UnityEngine;

public class Fall : MonoBehaviour, ICommand
{
    [SerializeField] private float gravity = 60f;
    private VerticalVelocityData data;

    private CharacterController body;

    private void Awake()
    {
        data = GetComponent<VerticalVelocityData>();
        body = GetComponent<CharacterController>();
    }

    public void Execute()
    {
        data.verticalVelocity -= gravity * Time.deltaTime;
        Vector3 jumpVelocity = new Vector3(0f, data.verticalVelocity, 0f);
        body.Move(jumpVelocity * Time.deltaTime);
    }
}
