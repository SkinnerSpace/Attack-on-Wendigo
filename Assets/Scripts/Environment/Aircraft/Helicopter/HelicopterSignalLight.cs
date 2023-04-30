using UnityEngine;

public class HelicopterSignalLight : MonoBehaviour, ISwitchable
{
    [Header("Required Components")]
    [SerializeField] private Helicopter helicopter;
    [SerializeField] private Transform passanger;
    
    [Header("Visual")]
    [SerializeField] private float lightHeight = 100f;
    [SerializeField] private float riseTime = 3f;

    private MeshRenderer lightRenderer;

    private bool isActive;
    private float time;
    private float lightHeightLerp;

    private Vector3 targetScale;

    private bool isRising;

    private void Awake()
    {
        helicopter.onLanded += SwitchOn;
        helicopter.onSetOff += SwitchOff;

        lightRenderer = GetComponent<MeshRenderer>();
        targetScale = new Vector3(1f, lightHeight, 1f);
    }

    private void Update()
    {
        if (isActive){
            Rise();
        }
    }

    private void Rise()
    {
        if (isRising)
        {
            CountDown();
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, lightHeightLerp); 
        }
    }

    private void CountDown()
    {
        time += Time.deltaTime;
        if (time >= riseTime){
            isRising = false;
        }

        lightHeightLerp = Mathf.InverseLerp(0f, riseTime, time);
        lightHeightLerp = Easing.QuadEaseInOut(lightHeightLerp);
    }

    public void SwitchOn()
    {
        isActive = true;
        isRising = true;
        time = 0f;

        lightRenderer.enabled = true;
        transform.position = helicopter.transform.position.FlatV3();
    }

    public void SwitchOff()
    {
        isActive = false;
        lightRenderer.enabled = false;
    }
}