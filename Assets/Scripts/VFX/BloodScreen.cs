using UnityEngine;

public class BloodScreen : MonoBehaviour
{
    private const float WIDTH = 900f;
    private const float HEIGHT = 500f;

    [Header("Count")]
    [SerializeField] private int minCount = 3;
    [SerializeField] private int maxCount = 5;
    [SerializeField] private int deathMultiplier = 2;

    [Header("Size")]
    [SerializeField] private float minSize = 600f;
    [SerializeField] private float maxSize = 1200f;

    [Header("Time")]
    [SerializeField] private float minTime = 0.5f;
    [SerializeField] private float maxTime = 1.5f;

    private IObjectPooler pooler;

    private void Start()
    {
        GameEvents.current.onBluntDamageReceived += () => ScatterTheScreenWithBlood(1);
        PlayerEvents.current.onDeath += () => ScatterTheScreenWithBlood(deathMultiplier);
        pooler = PoolHolder.Instance;
    }

    private void ScatterTheScreenWithBlood(int multiplier)
    {
        int count = Rand.Range(minCount * multiplier, maxCount * multiplier);

        for (int i = 0; i < count; i++){
            SpawnBloodSplatter();
        }
    }

    private void SpawnBloodSplatter()
    {
        RectTransform bloodSplatter = pooler.SpawnFromThePool("BloodSplatter").GetComponent<RectTransform>();
        BloodSplatter bloodSplatterComponent = bloodSplatter.GetComponent<BloodSplatter>();

        float time = Rand.Range(minTime, maxTime);
        bloodSplatterComponent.SetTime(time);

        bloodSplatter.SetParent(transform, false);
        bloodSplatter.anchoredPosition = GetRandomPosition();
        bloodSplatter.sizeDelta = GetSize();
        bloodSplatter.localEulerAngles = GetAngles();

        bloodSplatterComponent.Launch();
    }

    private Vector3 GetRandomPosition()
    {
        float xPosition = Rand.Range(-1f, 1f) * WIDTH;
        float yPosition = Rand.Range(-1f, 1f) * HEIGHT;
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
