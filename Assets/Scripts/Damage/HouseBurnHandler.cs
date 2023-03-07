using UnityEngine;

public class HouseBurnHandler : MonoBehaviour
{
    private FireHitBox fireHitBox;
    private BurnHandler burnHandler;
    private FunctionTimer timer;

    private float scorchTime = 1f;
    private int scorchDamage = 1;

    private void Awake()
    {
        fireHitBox = GetComponent<FireHitBox>();
        timer = GetComponent<FunctionTimer>();
        burnHandler = new BurnHandler(fireHitBox, timer, scorchTime);

    }
}
