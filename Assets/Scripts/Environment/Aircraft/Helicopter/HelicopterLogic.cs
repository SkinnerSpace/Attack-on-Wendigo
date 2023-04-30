using UnityEngine;

public class HelicopterLogic
{
    private Helicopter helicopter;
    private HelicopterData data;

    public HelicopterLogic(Helicopter helicopter, HelicopterData data)
    {
        this.helicopter = helicopter;
        this.data = data;
    }

    public void MakeADecision()
    {
        switch (data.state)
        {
            case HelicopterStates.Follow:
                helicopter.Fly();
                break;

            case HelicopterStates.IsGoingToLand:
                helicopter.Land();
                break;

            case HelicopterStates.Land:
                helicopter.Descend();
                break;
        }
    }
}
