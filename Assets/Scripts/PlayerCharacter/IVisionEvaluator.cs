using System;

public interface IVisionEvaluator
{
    bool IsSuitable(VisionTarget target);
    void AddSample(Type type, float distance);
}