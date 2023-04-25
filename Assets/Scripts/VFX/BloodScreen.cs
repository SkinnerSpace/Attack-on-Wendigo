using UnityEngine;

public class BloodScreen : MonoBehaviour
{
    [SerializeField] private GameObject bloodSplatter;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        GameEvents.current.onBluntDamageReceived += SplatterTheScreenWithBlood;
    }

    private void SplatterTheScreenWithBlood()
    {
        float xPosition = Rand.Range(0f, Screen.width);
        float yPosition = Rand.Range(0f, Screen.height);

        RectTransform bloodSplatterTransform = Instantiate(bloodSplatter, rectTransform).GetComponent<RectTransform>();
        bloodSplatterTransform.localPosition = new Vector3(xPosition, yPosition, 0f);
    }
}
