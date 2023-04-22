using TMPro;
using UnityEngine;

public class MenuTitle : MonoBehaviour
{
    private TextMeshProUGUI title;

    private void Awake()
    {
        title = GetComponent<TextMeshProUGUI>();
    }

    public void Set(string titleText)
    {
        title.text = titleText;
    }
}
