using System.Collections;
using UnityEngine;

public class PropDropper : MonoBehaviour
{
    private const float DEPTH_OFFSET = 1f;
    private const float PUSH_MULTIPLIER = 7f;

    private Vector3 fallOffset;
    private Vector3 fallDisplacement;

    private float time;
    private float currentTime;
    private float lerp;

    public void PrepareFall(float time, float height)
    {
        this.time = time;

        float depth = -(height + DEPTH_OFFSET);
        fallOffset = new Vector3(0f, depth, 0f);
    }

    public void SetDir(Vector3 direction)
    {
        Vector3 flatDir = new Vector3(direction.x, 0f, direction.z);
        fallOffset += (flatDir * PUSH_MULTIPLIER);
    }

    public void UpdateFall()
    {
        currentTime += Time.deltaTime;
        lerp = Mathf.Pow((currentTime / time), 2f);
        fallDisplacement = Vector3.Lerp(Vector3.zero, fallOffset, lerp);
    }

    public Vector3 GetFallDisplacement() => fallDisplacement;

    public bool IsDone() => currentTime >= time;
}

