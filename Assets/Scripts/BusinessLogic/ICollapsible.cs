using UnityEngine;

public interface ICollapsible
{
    Vector3 Position { get; }
    void PullDown(Vector3 direction);
}