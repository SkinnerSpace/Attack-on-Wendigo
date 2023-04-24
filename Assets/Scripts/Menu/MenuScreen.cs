using UnityEngine;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private float defaultWidth;
    [SerializeField] private float keyboardSettingsWidth;
    [SerializeField] private float transitionTime;

    private RectTransform rectTransform;

    private bool isChanging;
    private float time;
    private float lerp;

    private Vector2 originalSize;
    private Vector2 targetSize;

    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start(){
        MenuEvents.current.onSubMenuEnter += ChangeWidthAccordingToTheSubMenu;
    }

    private void Update()
    {
        if (isChanging){
            CountDown();
            UpdateWidth();
        }
    }

    private void CountDown()
    {
        time += Time.unscaledDeltaTime;
        lerp = Mathf.InverseLerp(0f, transitionTime, time);

        if (lerp >= 1f){
            isChanging = false;
        }
    }

    private void UpdateWidth(){
        rectTransform.sizeDelta = Vector2.Lerp(originalSize, targetSize, lerp);
    }

    private void SetTargetWidth(float targetWidth){
        targetSize = new Vector2(targetWidth, rectTransform.sizeDelta.y);
    }

    private void StartTransition()
    {
        originalSize = rectTransform.sizeDelta;
        time = 0f;
        isChanging = true;
    }

    private void ChangeWidthAccordingToTheSubMenu(string subMenuName)
    {
        if (subMenuName == "keyboard"){
            SetKeyboardSettingsWidth();
        }
        else if (subMenuName == "controls"){
            SetDefaultWidth();
        }
    }

    private void SetDefaultWidth()
    {
        SetTargetWidth(defaultWidth);
        StartTransition();
    }

    private void SetKeyboardSettingsWidth()
    {
        SetTargetWidth(keyboardSettingsWidth);
        StartTransition();
    }
}
