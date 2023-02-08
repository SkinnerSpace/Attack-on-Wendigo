using System;
using UnityEngine;

public class Dispenser : MonoBehaviour, IHelicopterDoorObserver
{
    private DispenserData data;
    [SerializeField] private DispenserStorage storage;
    [SerializeField] private HelicopterDoor door;
    [SerializeField] private FunctionTimer timer;

    private GameObject item;
    private Crate crate;
    private Action notifyOnComplete;

    private void Awake() => door.Subscribe(this);

    public void SetData(DispenserData data) => this.data = data;

    public void Launch(GameObject cratePrefab, Action notifyOnComplete)
    {
        this.item = cratePrefab;
        this.notifyOnComplete = notifyOnComplete;
        crate = Instantiate(cratePrefab, transform.position, transform.rotation, data.DropSpace).GetComponent<Crate>();
        crate.Pack(storage.GetAnItem());
        
        door.Open();
    }

    public void OnDoorHasOpened() => Drop();

    public void Drop()
    {
        crate.Throw(data.ThrowFoce);
        timer.Set("Close", data.WaitTime, door.Close);
    }

    public void OnDoorHasClosed() => notifyOnComplete();
}
