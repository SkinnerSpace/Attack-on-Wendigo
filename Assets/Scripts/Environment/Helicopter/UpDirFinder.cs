using UnityEngine;

public class UpDirFinder : MonoBehaviour
{
    private Vector3[] directions = new Vector3[3];
    float highestUpwardness = 0f;
    int upwardSide = 0;
    int sign = 1;

    public Vector3 GetUpwardDirection()
    {
        UpdateDirections();

        for (int i = 0; i < directions.Length; i++)
            FindUpwardSide(i);

        return directions[upwardSide] * sign;
    }

    private void UpdateDirections()
    {
        directions[0] = transform.right;
        directions[1] = transform.up;
        directions[2] = transform.forward;
        highestUpwardness = 0f;
    }

    private void FindUpwardSide(int index)
    {
        float upwardness = Vector3.Dot(directions[index], Vector3.up);
        float unsignedUpwardness = Mathf.Abs(upwardness);

        if (unsignedUpwardness >= highestUpwardness)
        {
            highestUpwardness = unsignedUpwardness;
            sign = (int)Mathf.Sign(upwardness);
            upwardSide = index;
        }
    }
}
