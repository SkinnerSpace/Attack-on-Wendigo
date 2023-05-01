using UnityEngine;

public class Charger : MonoBehaviour
{
    [SerializeField] private float maxPower = 100f;
    [SerializeField] private float chargeTime = 3f;

    private float currentTime;
    public float power { get; private set; }
    public float charge { get; private set; }

    public void Charge()
    {
        currentTime += OldChronos.DeltaTime;
        charge = GetCharge(currentTime);
        power = GetPower(charge);
    }

    private float GetCharge(float time) =>  Mathf.Sqrt(Mathf.InverseLerp(0f, chargeTime, time));
    private float GetPower(float charge) => Mathf.Lerp(0f, maxPower, charge);

    public void Discharge()
    {
        currentTime = 0f;
        power = 0f;
    }
}