using System.Collections.Generic;

public interface IProp
{
    IPropData data { get; }
    List<ITransform> transforms { get; }
}