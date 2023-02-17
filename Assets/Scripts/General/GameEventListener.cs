using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;
    [SerializeField] UnityEvent unityEvent;

    private void Awake() => gameEvent.Subscribe(this);

    private void OnDestroy() => gameEvent.Unsubscribe(this);

    public void RaiseEvent() => unityEvent.Invoke();
}
