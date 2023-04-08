using UnityEngine;

public class GameBuildSettings : MonoBehaviour
{
    [SerializeField] private bool buildTheTown;
    [SerializeField] private GameBuildComponents components;

    private IArchitect architect;

    private void Awake()
    {
        architect = components.architect.GetComponent<IArchitect>();

        if (buildTheTown){
            architect.BuildTheTown();
        }
    }
}
