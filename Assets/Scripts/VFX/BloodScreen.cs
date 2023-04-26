using UnityEngine;

public class BloodScreen : MonoBehaviour
{
    private const float POSITION_OFFSET = 60f;

    [SerializeField] private float minSize = 600f;
    [SerializeField] private float maxSize = 1200f;

    private RectTransform rectTransform;

    private IObjectPooler pooler;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        GameEvents.current.onBluntDamageReceived += SplatterTheScreenWithBlood;
        pooler = PoolHolder.Instance;
    }

    private void SplatterTheScreenWithBlood()
    {
        RectTransform bloodSplatter = pooler.SpawnFromThePool("BloodSplatter").GetComponent<RectTransform>();

        bloodSplatter.SetParent(transform, false);
        bloodSplatter.localPosition = GetRandomPosition();
        bloodSplatter.sizeDelta = GetSize();
        bloodSplatter.localEulerAngles = GetAngles();
    }

    private Vector3 GetRandomPosition()
    {
        float xPosition = Rand.Range(POSITION_OFFSET, Screen.width - POSITION_OFFSET);
        float yPosition = Rand.Range(POSITION_OFFSET, -Screen.height + POSITION_OFFSET);
        Vector3 position = new Vector3(xPosition, yPosition, 0f);

        return position;
    }

    private Vector2 GetSize()
    {
        float width = Rand.Range(minSize, maxSize);
        float height = Rand.Range(minSize, maxSize);
        Vector2 size = new Vector2(width, height);

        return size;
    }

    private Vector3 GetAngles()
    {
        float angle = Rand.Range(0f, 360f);
        return new Vector3(0f, 0f, angle);
    }
}
