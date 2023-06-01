using System.Collections;
using UnityEngine;

public abstract class InputReader : MonoBehaviour
{
    protected UnityInputReader input;
    protected KeyBinds keys;

    protected virtual void ReadInputWhenActive()
    {
        if (GlobalData.PauseMode == PauseMode.None){
            ReadInput();
        }
    }

    protected abstract void ReadInput();

    private void Awake()
    {
        GetComponent<MainInputReader>().Add(this);
        input = GetComponent<UnityInputReader>();
        keys = GetComponent<KeyBinds>();
    }
}
