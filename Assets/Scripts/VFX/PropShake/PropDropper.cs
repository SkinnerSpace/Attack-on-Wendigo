using System.Collections;
using UnityEngine;

public class PropDropper : MonoBehaviour
{
    [SerializeField] private Vector3 fallOffset;
    private Vector3 fallDisplacement;

    private float time;
    private float currentTime;
    private float lerp;

    public void PrepareFall(float time) => this.time = time;

    public void UpdateFall()
    {
        currentTime += Time.deltaTime;
        lerp = currentTime / time;
        fallDisplacement = Vector3.Lerp(Vector3.zero, fallOffset, lerp);
    }

    public Vector3 GetFallDisplacement() => fallDisplacement;

    public bool IsDone() => currentTime >= time;
}

