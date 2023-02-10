using System;
using UnityEngine;

public class Dispenser : MonoBehaviour, IHelicopterDoorObserver
{
    private DispenserData data;
    [SerializeField] private DispenserStorage storage;
    [SerializeField] private HelicopterDoor door;
    [SerializeField] private FunctionTimer timer;


    private Crate crate;
    private Action notifyOnComplete;

    private void Awake() => door.Subscribe(this);

    public void SetData(DispenserData data) => this.data = data;

    public void Launch(GameObject crateObject, Action notifyOnComplete)
    {
        this.notifyOnComplete = notifyOnComplete;

        crateObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
        crate = crateObject.GetComponent<Crate>();
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
