using UnityEngine;

public class Arms : MonoBehaviour
{
    [SerializeField] private Transform leftHandTarget;
    [SerializeField] private Transform rightHandTarget;

    private HandPoint leftHandPoint;
    private HandPoint rightHandPoint;

    public void SetWeapon(IWeapon weapon)
    {
        leftHandPoint = weapon.GetHandPoints()["Left"];
        rightHandPoint = weapon.GetHandPoints()["Right"];
    }

    private void Update() => UpdatePosition();

    private void UpdatePosition()
    {
        if (leftHandPoint != null) leftHandTarget.position = leftHandPoint.Position; //leftHandTarget.SetPositionAndRotation(leftHandPoint.Position, leftHandPoint.Rotation);
        if (rightHandPoint != null) rightHandTarget.position = rightHandTarget.position; //rightHandTarget.SetPositionAndRotation(rightHandPoint.Position, rightHandPoint.Rotation);
    }
}
