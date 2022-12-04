using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    public int buildings { get; private set; }
    private bool active;

    public void SetActive(bool active)
    {
        this.active = active;
        gameObject.SetActive(active);

        if (!active)
            buildings = 0;

        ShowCount();
    }

    private void OnTriggerEnter(Collider other)
    {
        IBuilding building = other.transform as IBuilding;

        if (building != null)
            buildings += 1;

        ShowCount();
    }

    private void OnTriggerExit(Collider other)
    {
        IBuilding building = other.transform as IBuilding;

        if (building != null)
            buildings -= 1;

        ShowCount();
    }

    private void ShowCount()
    {
        Debug.Log("Objects " + buildings);
    }
}
