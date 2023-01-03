using UnityEngine;

public class WeaponRecoilController : MonoBehaviour
{
    [SerializeField] private float recoilPower = 2f;
    [SerializeField] private float recoilSpeed = 10f;
    [SerializeField] private float recoilDeceleration = 10f;

    private Vector3 recoilVelocity;

    private void Update()
    {
        UpdateRecoil();
    }

    private void UpdateRecoil()
    {
        recoilVelocity = Vector3.Lerp(recoilVelocity, Vector3.zero, recoilDeceleration * Time.deltaTime);
        transform.localPosition = Vector3.Lerp(transform.localPosition, recoilVelocity, recoilSpeed * Time.deltaTime);
    }

    public void Recoil()
    {
        recoilVelocity = new Vector3(0f, 0f, -1f) * recoilPower;
    }
}
