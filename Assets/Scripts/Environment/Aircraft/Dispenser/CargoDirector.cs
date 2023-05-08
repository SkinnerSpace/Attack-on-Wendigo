using UnityEngine;

public class CargoDirector : MonoBehaviour
{
    private const int maxItemWeight = 10;

    [SerializeField] private AnimationCurve aidKitDropProbability;
    [SerializeField] private string aidKit;
    [SerializeField] CargoItem[] items;

    private float healthDeficiency;
    private float progress;
    private int previousItemIndex = -1;

    private bool indexIsReserved;
    private int reservedIndex;

    public static CargoDirector Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerEvents.current.onHealthUpdate += (int playerHealth) => healthDeficiency = 1f - (playerHealth / 100f);
        GameEvents.current.onDeathProgressUpdate += (float progress) => this.progress = progress;
    }

    public string GetItem()
    {
        if (indexIsReserved){
            indexIsReserved = false;
            return items[reservedIndex].poolName;
        }

        if (MustDropAidKit()){
            return aidKit;
        }

        return GetRandomItem(items);
    }

    private bool MustDropAidKit() => Rand.Range01() < aidKitDropProbability.Evaluate(healthDeficiency);

    public void ReserveItem(int reservedIndex)
    {
        this.reservedIndex = reservedIndex;
        indexIsReserved = true;
    }

    private string GetRandomItem(CargoItem[] untestedCargo)
    {
        int weightSum = GetWeightSum(untestedCargo);
        int index = GetRandomUniqueItemIndex(untestedCargo, weightSum);
        string item = items[index].poolName;

        return item;
    }

    private int GetWeightSum(CargoItem[] untestedCargo)
    {
        int weightSum = 0;

        for (int i = 0; i < untestedCargo.Length; ++i)
        {
            weightSum += GetItemWeight(untestedCargo[i]);
        }

        return weightSum;
    }

    private int GetRandomUniqueItemIndex(CargoItem[] untestedCargo, int weightSum)
    {
        int index = previousItemIndex;

        while (index == previousItemIndex)
        {
            index = GetRandomWeightedItemIndex(untestedCargo, weightSum);
        }

        previousItemIndex = index;

        return index;
    }

    private int GetRandomWeightedItemIndex(CargoItem[] untestedCargo, int weightSum)
    {
        int index = 0;
        int lastIndex = untestedCargo.Length - 1;

        while (index < lastIndex)
        {
            if (CheckProbability(weightSum, untestedCargo[index]))
            {
                return index;
            }

            weightSum -= GetItemWeight(untestedCargo[index++]);
        }

        return index;
    }

    private bool CheckProbability(int weightSum, CargoItem item) => Rand.Range(0, weightSum) < GetItemWeight(item);
    private int GetItemWeight(CargoItem item) => (int) (item.weight.Evaluate(progress) * maxItemWeight);
}