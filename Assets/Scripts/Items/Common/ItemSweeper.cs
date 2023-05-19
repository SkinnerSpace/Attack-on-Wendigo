using System;
using System.Collections;
using UnityEngine;

public class ItemSweeper : MonoBehaviour
{
    private const float SWEPT_HEIGHT = -4f;

    [Header("Required Components")]
    [SerializeField] private ItemPhysicalBody physics;
    [SerializeField] private FunctionTimer timer;
    [SerializeField] private SweeperData data;

    private IPooledObject pooledObject;

    private bool isSweeping;
    private bool isFalling;

    private void Awake()
    {
        pooledObject = GetComponent<IPooledObject>();
    }

    public void SweepTheWeapon()
    {
        if (!isSweeping){
            isSweeping = true;

            StartCoroutine(WaitForRest());
        }
    }

    private IEnumerator WaitForRest()
    {
        while (physics.Velocity.magnitude > 0.05f){
            yield return null;
        }

        StartFalling();
    }

    private void StartFalling()
    {
        isFalling = true;
        physics.DisablePhysics();
    }

    private void Update()
    {
        if (isFalling)
        {
            MoveDown();

            if (IsBuriedUnderTheGround()){
                FinishSweeping();
            }
        }
    }

    private void MoveDown() => transform.position += Vector3.down * data.fallSpeed * Time.deltaTime;

    private bool IsBuriedUnderTheGround()
    {
        return transform.position.y <= SWEPT_HEIGHT;
    }

    private void FinishSweeping()
    {
        isFalling = false;
        isSweeping = false;
        pooledObject.BackToPool();
    }

}
