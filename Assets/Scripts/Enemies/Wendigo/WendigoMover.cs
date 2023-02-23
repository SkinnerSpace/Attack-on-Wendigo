using UnityEngine;

public class WendigoMover : MonoBehaviour
{
    private Wendigo wendigo;
    private WendigoData data;
    private CharacterController controller;
    private IChronos chronos;

    public void Initialize(Wendigo wendigo)
    {
        controller = wendigo.Controller;
        data = wendigo.Data;
    }

    private void Update()
    {
        controller.Move(data.Velocity * chronos.DeltaTime);
    }
}
