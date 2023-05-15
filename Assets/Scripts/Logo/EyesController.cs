using UnityEngine;

[ExecuteAlways]
public class EyesController : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float dilation;
    [SerializeField] private float minSize;

    [SerializeField] private RectTransform[] eyes;

    public void SetDilation(float dilation)
    {
        this.dilation = dilation;
        UpdateSize();
    }

    private void UpdateSize()
    {
        float size = Mathf.Lerp(minSize, 1f, dilation);
        size = Easing.QuadEaseInOut(size);

        foreach (RectTransform eye in eyes)
        {
            eye.localScale = new Vector2(size, size);
        }
    }
}
