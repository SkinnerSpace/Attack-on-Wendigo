using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drugs : MonoBehaviour, IHandyItem, IHealthPack
{
    private IInputReader inputReader;
    private FunctionTimer timer;

    private Action onReady;

    public Vector3 DefaultPosition => new Vector3(0.75f, -0.35f, 1.5f);

    public void InitializeOnTake(ICharacterData characterData, IInputReader inputReader)
    {
        this.inputReader = inputReader;

    }

    public void SetReady(bool isReady)
    {
        ManageConnectionToInput(isReady);

        if (isReady){
            onReady?.Invoke();
        }
    }

    public void SubscribeOnReady(Action onReady) => this.onReady += onReady;

    public void Use()
    {
        Debug.Log("INJECT");
    }

    private void ManageConnectionToInput(bool connect)
    {
        if (inputReader != null)
        {
            if (connect){
                inputReader.Get<CombatInputReader>().Subscribe(this);
            }
            else{
                inputReader.Get<CombatInputReader>().Unsubscribe(this);
            }
        }
    }
}
