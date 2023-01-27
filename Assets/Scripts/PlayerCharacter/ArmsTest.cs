using UnityEngine;

public class ArmsTest : MonoBehaviour
{
    public enum Modes
    {
        Off,
        Default,
        Test
    }

    [Header("Settings")]
    [SerializeField] private Modes mode = Modes.Off;

    [Header("Targets")]
    [SerializeField] private Transform leftHandTarget;
    [SerializeField] private Vector3 leftTargetDefaultPos = new Vector3(-0.76f, 1.5f, 0f);

    [SerializeField] private Transform rightHandTarget;
    [SerializeField] private Vector3 rightTargetDefaultPos = new Vector3(0.76f, 1.5f, 0f);

    [Header("Points")]
    [SerializeField] private HandPoint leftHandPoint;
    [SerializeField] private HandPoint rightHandPoint;

    private void OnDrawGizmos() => TestPose();

    private void TestPose()
    {
        if (mode == Modes.Default)
        {
            leftHandTarget.localPosition = leftTargetDefaultPos;
            leftHandTarget.localRotation = Quaternion.Euler(0, 0, 0);

            rightHandTarget.localPosition = rightTargetDefaultPos;
            rightHandTarget.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (mode == Modes.Test)
        {
            if (leftHandPoint != null)
            {
                leftHandTarget.position = leftHandPoint.transform.position;
                leftHandTarget.eulerAngles = leftHandPoint.transform.eulerAngles;
            }
           
            if (rightHandPoint != null)
            {
                rightHandTarget.position = rightHandPoint.transform.position;
                rightHandTarget.eulerAngles = rightHandTarget.transform.eulerAngles;
            }
        }
    }
}
