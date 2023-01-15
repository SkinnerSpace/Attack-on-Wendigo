using System.Collections.Generic;
using UnityEngine;

public class PropRotator : MonoBehaviour
{
    [SerializeField] Transform prop;
    [SerializeField] private List<float> angles = new List<float>();

    private void Awake() => SetAngle();

    private void SetAngle()
    {
        if (angles.Count > 0) SetConstrainedAngle();
        else SetLooseAngle();
    }

    private void SetConstrainedAngle()
    {
        int index = Rand.Range(0, angles.Count);
        prop.eulerAngles = new Vector3(0f, angles[index], 0f);
    }

    private void SetLooseAngle()
    {
        float yAngle = Rand.Range(0f, 360f);
        prop.eulerAngles = new Vector3(0f, yAngle, 0f);
    }
}
