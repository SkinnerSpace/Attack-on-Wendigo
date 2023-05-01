using System;

public class DrugsPooledObject : ItemPooledObject
{
    private Drugs drugs;
    public event Action onSpawn;

    protected override void Awake()
    {
        base.Awake();
        drugs = GetComponent<Drugs>();
    }

    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        drugs.Refill();
        onSpawn?.Invoke();
    }
}