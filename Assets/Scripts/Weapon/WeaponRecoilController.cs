using UnityEngine;

public class WeaponRecoilController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float recoilPower = 2f;
    [SerializeField] private float recoilTime = 1f;

    private Vector3 RecoiledPosition => -1 * Vector3.forward * recoilPower;
    private float currentTime;

    private bool isRecoiling;

    private void Update() => UpdateRecoil();

    private void UpdateRecoil()
    {
        if (isRecoiling) Move();
    }

    public void Recoil()
    {
        isRecoiling = true;
        currentTime = recoilTime;
    }

    private void Move()
    {
        CountDown();
        float percent = GetRecoilPercent();
        transform.localPosition = Vector3.Lerp(Vector3.zero, RecoiledPosition, percent);
    }

    private void CountDown()
    {
        currentTime -= Chronos.DeltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isRecoiling = false;
        }
    }

    private float GetRecoilPercent()
    {
        float percent = Mathf.InverseLerp(0f, recoilTime, currentTime);
        float lerp = Mathf.Lerp(-0.5f, 1f, percent);
        return Easing.QuadEaseOut(lerp);
    }
}
