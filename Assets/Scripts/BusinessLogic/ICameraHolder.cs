using UnityEngine;

public interface ICameraHolder
{
    void SetGameMode(Transform pivot);
    void SetDemoMode(Transform pivot);
    void SetOutroMode(Transform pivot);
}
