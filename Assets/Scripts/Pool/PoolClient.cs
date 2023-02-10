using UnityEngine;
using System.Collections.Generic;

public class PoolClient : MonoBehaviour
{
    [SerializeField] private List<PoolTemplate> poolTemplates;

    private void Start() => PoolHolder.Instance.ExecutePoolTemplates(poolTemplates);
}
