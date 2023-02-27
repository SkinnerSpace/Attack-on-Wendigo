using UnityEngine;
using TMPro;

public class TimeCountGUI : MonoBehaviour, ITimeObserver
{
    [SerializeField] private InvasionCounter counter;
    [SerializeField] private TextMeshProUGUI display;

    private void Awake()
    {
        counter.SubscribeOnUpdate(OnTimeUpdate);
    }

    public void OnTimeUpdate(int time)
    {
        display.text = time.ToString();
        //display.rectTransform.localScale += Vector3.one;
    }
}