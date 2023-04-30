using UnityEngine;

public class EndingController : MonoBehaviour
{
    [SerializeField] private LoadingScreenText loadingScreenText;

    private void Start()
    {
        HelicopterEvents.current.onFlewAway += FinishTheGame;
    }

    private void FinishTheGame()
    {
/*        MenuEvents.current.RestartTheGame();
        loadingScreenText.SetEndingText();*/
    }
}