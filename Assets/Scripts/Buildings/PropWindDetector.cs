using UnityEngine;

public class PropWindDetector : MonoBehaviour
{
    [SerializeField] private PropPuller puller;
    [SerializeField] private CollapseController collapseController;

    private void Start()
    {
        GameEvents.current.onBlizzardRadiusUpdate += FlyAwayInCaseOfTheWind;
    }

    private void FlyAwayInCaseOfTheWind(float radius, Vector3 point)
    {
        float distance = Vector2.Distance(transform.position.FlatV2(), point.FlatV2());

        if (distance > radius)
        {
            puller.SwitchOn();
            collapseController.SwitchOff();
        }
    }
}
