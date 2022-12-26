using System.Collections.Generic;

public interface IProp
{
    IPropData data { get; }
    List<ITransformProxy> transforms { get; }
}