using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StateController : MonoBehaviour, IObserver
{
    [NonSerialized] public Player player;
    [NonSerialized] public State currentState;

    public Dictionary<Type, State> states = new Dictionary<Type, State>();

    private void Awake()
    {
        player = GetComponent<Player>();
        currentState = new NormalState();
    }

    private void Update()
    {
        currentState.Execute(this);
    }

    public void SwitchState(Type type)
    {
        currentState = states[type];
    }

    public void FeedBack(Type type)
    {
        Debug.Log("FeedBack " + type);

        if (currentState.GetType() == typeof(NormalState))
        {
            if (type == typeof(Hook))
            {
                Debug.Log("SWITCH STATE");
                //currentState = states[typeof(HookLaunchState)];
            }
        }
    }
}