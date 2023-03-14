using System;
using UnityEngine;
using WendigoCharacter;

public class FireballSpawnerComponent : MonoBehaviour
{
    [SerializeField] private WendigoData data;
    [SerializeField] private ParticleSystem castVFX;
    private IObjectPooler pooler;

    private float XPositive => data.FireballSpawner.VerticalAngle / 2f;
    private float XNegative => 360f - XPositive;

    private float YPositive => data.FireballSpawner.HorizontalAngle / 2f;
    private float YNegative => 360f - YPositive;

    private void Start() => pooler = PoolHolder.Instance;

    public void PlayCastVFX() => castVFX.Play();

    public void SpawnFireball()
    {
        Rotate();
        pooler.SpawnFromThePool("Fireball", transform.position, transform.rotation);
    }

    private void Rotate()
    {
        transform.LookAt(data.Target);

        float xAngle = ConstrainXAngle(transform.localEulerAngles.x);
        float yAngle = ConstrainYAngle(transform.localEulerAngles.y);

        transform.localEulerAngles = new Vector3(xAngle, yAngle, 0f);
    }

    private float ConstrainXAngle(float xAngle)
    {
        if (xAngle < 180f && xAngle >= XPositive)
            xAngle = XPositive;

        if (xAngle > 180f && xAngle <= XNegative)
            xAngle = XNegative;

        return xAngle;
    }

    private float ConstrainYAngle(float yAngle)
    {
        if (yAngle < 180f && yAngle >= YPositive)
            yAngle = YPositive;

        if (yAngle > 180f && yAngle <= YNegative)
            yAngle = YNegative;

        return yAngle;
    }
}

public class FireballSpawner
{

}