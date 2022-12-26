using System.Collections.Generic;

public class LegsSync : ILegsSync
{
    public List<ILeg> legs { get; set; }
    public int Index { get; set; }
    public ILeg CurrentLeg { get; set; }
    public List<IStepMagnitudeObserver> stepMagnitudeObservers = new List<IStepMagnitudeObserver>();

    public LegsSync(List<ILeg> legs)
    {
        this.legs = legs;
    }

    public void Walk()
    {
        if (legs.Count > 0)
        {
            CurrentLeg = legs[Index];
            CurrentLeg.SetNextStep();

            foreach (ILeg leg in legs)
                leg.Update();

            NotifyStepMagnitudeObservers();
        }
    }

    public void StepIsOver()
    {
        Index += 1;
        if (Index > legs.Count - 1)
            Index = 0;
    }

    public void AddStepMagnitudeObserver(IStepMagnitudeObserver observer)
    {
        stepMagnitudeObservers.Add(observer);
    }

    public void NotifyStepMagnitudeObservers()
    {
        float stepMagnitude = CurrentLeg.Side * CurrentLeg.Transform.Position.y;

        foreach (IStepMagnitudeObserver observer in stepMagnitudeObservers)
            observer.ReceiveStepMagnitude(stepMagnitude);
    }
}