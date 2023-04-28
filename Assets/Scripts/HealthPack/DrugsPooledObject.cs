public class DrugsPooledObject : ItemPooledObject
{
    private Drugs drugs;

    protected override void Awake()
    {
        base.Awake();
        drugs = GetComponent<Drugs>();
    }

    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        drugs.Refill();
    }
}