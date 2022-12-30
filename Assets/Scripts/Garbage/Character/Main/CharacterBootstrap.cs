using UnityEngine;

public class CharacterBootstrap : MonoBehaviour
{
    public void Bootstrap(Character character)
    {
        character.controller = ControllerFactory.Create(character.controllerType);

        character.data = GetComponent<CharacterData>();
        character.body = GetComponent<CharacterController>();
        character.movement = transform.Find("Movement").GetComponent<Movement>();
        character.hookshot = transform.Find("Hookshot").GetComponent<Hookshot>();

        character.mainCamera = transform.Find("Camera").GetComponent<Camera>();
        character.cameraLook = character.mainCamera.GetComponent<CameraLook>();
        character.cameraFov = character.mainCamera.GetComponent<CameraFov>();

        SetDependee(character);
    }

    private void SetDependee(Character character)
    {
        ICharacterDependee[] components = transform.GetComponentsInChildren<ICharacterDependee>();

        for (int i = 0; i < components.Length; i++)
            components[i].SetUp(character);
    }
}