using UnityEngine;

public class CameraHolder : MonoBehaviour, ICameraHolder
{
    public enum States
    {
        Demo,
        Gameplay,
        Outro
    }

    [SerializeField] private Camera cam;
    [SerializeField] private Transform pivot;
    [SerializeField] private States state;

    [SerializeField] private bool testMode;

    [Header("Influence")]
    [SerializeField] private float maxInfluence;
    [SerializeField] private float influenceEaseTime;

    private bool isEasingTheInfluence;
    private float influenceLerp;
    private float influence;
    private float currentInfluenceEaseTime;

    void Update()
    {
        Follow();
    }

    public void Follow()
    {
        if (pivot != null)
        {
            if (state != States.Outro){
                transform.position = pivot.position;
                transform.rotation = pivot.rotation;
            }
            else if (state == States.Outro){
                UpdateOutro();
            }
        }
    }

    private void UpdateOutro()
    {
        if (isEasingTheInfluence){
            currentInfluenceEaseTime += Time.deltaTime;
            if (currentInfluenceEaseTime >= influenceEaseTime){
                isEasingTheInfluence = false;
            }

            influenceLerp = Mathf.InverseLerp(0f, influenceEaseTime, currentInfluenceEaseTime);
            influenceLerp = Easing.QuadEaseOut(influenceLerp);

            influence = Mathf.Lerp(maxInfluence, 0f, influenceLerp);
        }

        ResetCameraTransform();
        transform.position = Vector3.Lerp(transform.position, pivot.position, influence * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, pivot.rotation, influence * Time.deltaTime);
    }

    public void SetGameMode(Transform pivot)
    {
        state = States.Gameplay;
        this.pivot = pivot;
    }

    public void SetDemoMode(Transform pivot)
    {
        state = States.Demo;
        this.pivot = pivot;
    }

    public void SetOutroMode(Transform pivot)
    {
        state = States.Outro;
        isEasingTheInfluence = true;
        ResetCameraTransform();
        this.pivot = pivot;
    }

    private void ResetCameraTransform()
    {
        cam.transform.localRotation = Quaternion.identity;
        cam.transform.localPosition = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        if (testMode) Follow();
    }
}
