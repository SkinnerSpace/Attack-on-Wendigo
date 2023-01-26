using UnityEngine;

public class WeaponRecoilController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float recoilPower = 2f;
    [SerializeField] private float recoilSpeed = 10f;
    [SerializeField] private float recoilDeceleration = 10f;

    private Vector3 recoilVelocity;
    private bool isRecoiling;

    private void Update() => UpdateRecoil();

    private void UpdateRecoil()
    {
        if (isRecoiling)
        {
            Move();
            StopRecoil();
        }
    }

    public void Recoil()
    {
        isRecoiling = true;
        recoilVelocity = new Vector3(0f, 0f, -1f) * recoilPower;
    }

    private void Move()
    {
        recoilVelocity = Vector3.Lerp(recoilVelocity, Vector3.zero, recoilDeceleration * Time.deltaTime);
        transform.localPosition = Vector3.Lerp(transform.localPosition, recoilVelocity, recoilSpeed * Time.deltaTime);
    }

    private void StopRecoil()
    {
        if (recoilVelocity.magnitude <= 0.01f)
        {
            isRecoiling = false;
            transform.localPosition = Vector3.zero;
        }
    }
}
