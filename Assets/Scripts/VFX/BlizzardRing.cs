using UnityEngine;

public class BlizzardRing : MonoBehaviour
{
    [SerializeField] private Vector3 ratio;
    [SerializeField] private float influence;

    private void Awake()
    {
        
    }

    public void SetScale(Vector3 scale){
        Vector3 modifiedScale = scale * influence;
        Vector3 appropriateScale = Vector3.Scale(modifiedScale, ratio);
        transform.localScale = appropriateScale;
    }
}