using System.Collections.Generic;
using UnityEngine;

public interface IObjectPooler
{
    void ExecutePoolTemplates(List<PoolTemplate> poolTemplates);
    GameObject SpawnFromThePool(string tag, Vector3 position, Quaternion rotation);
    GameObject SpawnFromThePool(string tag);
}