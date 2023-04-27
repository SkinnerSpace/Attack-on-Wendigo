using UnityEngine;

public class Levitator : MonoBehaviour, ISwitchable
{
    private const float MAX_SIN_TIME = Mathf.PI * 2f;

    [SerializeField] private LevitatorSharedData sharedData;
    [SerializeField] private float leviationHeight = 1.5f;

    private ItemPhysicalBody physicalBody;

    private float time;
    private float localHeight;

    private bool isLevitating;

    private void Awake(){
        physicalBody = GetComponent<ItemPhysicalBody>();
        physicalBody.onDisabled += SwitchOff;
    }

    public void SwitchOn() => isLevitating = true;

    public void SwitchOff() => isLevitating = false;

    private void Update()
    {
        if (isLevitating){
            Levitate();
        }
    }

    public void Levitate()
    {
        CountTime(sharedData.frequency);
        localHeight = Mathf.Sin(time) * sharedData.magnitude;

        PushOffTheGround();
        Rotate();
    }

    private void CountTime(float frequency)
    {
        time += frequency * Time.deltaTime;

        if (time > MAX_SIN_TIME)
            time -= MAX_SIN_TIME;
    }

    private void PushOffTheGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, ComplexLayers.Landscape))
        {
            float currentHeight = transform.position.y - localHeight;
            float targetHeight = hit.point.y + leviationHeight;

            float adjustedHeight = Mathf.Lerp(currentHeight, targetHeight, sharedData.pushSpeed * Time.deltaTime);
            transform.position = transform.position.SetY(adjustedHeight + localHeight);
        }
    }

    private void Rotate(){
        transform.eulerAngles += new Vector3(0f, sharedData.rotationSpeed, 0f) * Time.deltaTime;
    }
}
