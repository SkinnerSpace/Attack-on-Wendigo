public class WeaponPooledObject : ItemPooledObject
{
    private Weapon weapon;

    protected override void Awake()
    {
        base.Awake();
        weapon = GetComponent<Weapon>();
    }

    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        weapon.Reload();
    }
}


