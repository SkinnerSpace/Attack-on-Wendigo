using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FunctionTimerTests : MonoBehaviour
{
    private const string FIRST_TIMER = "First timer";
    private const string SECOND_TIMER = "Second timer";
    private const string THIRD_TIMER = "Third timer";

    private FunctionTimer timer;

    private void Awake()
    {
        timer = GetComponent<FunctionTimer>();

        timer.Set(FIRST_TIMER, 3f, FirstAction);
        timer.Set(SECOND_TIMER, 6f, SecondAction);
        timer.Set(THIRD_TIMER, 9f, ThirdAction);
    }

    private void FirstAction()
    {
        Debug.Log("First action");

        if (!timer.TimerExist(SECOND_TIMER))
        {
            Debug.Log(SECOND_TIMER + " doesn't exist");
        }

        timer.Stop(SECOND_TIMER);
    }

    private void SecondAction()
    {
        Debug.Log("Second action");

        if (timer.TimerExist(FIRST_TIMER))
        {
            Debug.Log(FIRST_TIMER + " exist");
        }
        else
        {
            Debug.Log(FIRST_TIMER + " doesn't exist");
        }
    }

    private void ThirdAction()
    {
        Debug.Log("Third action");
        timer.Set(FIRST_TIMER, 3f, FirstAction);
    }
}
