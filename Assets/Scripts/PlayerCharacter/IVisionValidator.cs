using System;

public interface IVisionValidator
{
    VisionTarget Validate(VisionTarget target);
    void AddSample(Type type, float distance);
}