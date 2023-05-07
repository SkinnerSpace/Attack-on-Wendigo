using UnityEngine;

public class Director : MonoBehaviour
{
    [SerializeField] private AnimationCurve replacementProbability;
    [SerializeField] private string replacementItem;

    private float healthDeficiency;

    public static Director Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerEvents.current.onHealthUpdate += (int playerHealth) => healthDeficiency = 1f - (playerHealth / 100f);
    }

    public string ReplaceItemIfNecessary(string originalItem)
    {
        float probability = replacementProbability.Evaluate(healthDeficiency);

        if (Rand.Range01() < probability){
            return replacementItem;
        }

        return originalItem;
    }
}