using UnityEngine;

public class MenuScreen : MonoBehaviour
{
    [Header("Width")]
    [SerializeField] private float defaultWidth;
    [SerializeField] private float keyboardSettingsWidth;
    [SerializeField] private float gameOverWidth;

    [Header("Time")]
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

    private void ChangeWidthAccordingToTheSubMenu(string subMenuName)
    {
        if (subMenuName == "keyboard"){
            SetTargetWidth(keyboardSettingsWidth);
        }

        else if (subMenuName == "controls"){
            SetTargetWidth(defaultWidth);
        }

        else if (subMenuName == "gameover"){
            SetTargetWidth(gameOverWidth);
        }
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
        StartTransition();
    }

    private void StartTransition()
    {
        originalSize = rectTransform.sizeDelta;
        time = 0f;
        isChanging = true;
    }
}
