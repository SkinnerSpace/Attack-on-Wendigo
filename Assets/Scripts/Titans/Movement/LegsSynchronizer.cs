using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LegsSynchronizer : MonoBehaviour
{
    private bool active;
    [SerializeField] private List<Leg> legs = new List<Leg>();
    private int currentLeg = 0;

    [SerializeField] private Titan titan;
    public Vector3 bodyPosition { get; private set; }

    private void Awake()
    {
        bodyPosition = titan.transform.position;

        foreach (Leg leg in legs)
            leg.SetSynchronizer(this);
    }

    public void SetBodyPosition(Vector3 bodyPosition)
    {
        this.bodyPosition = bodyPosition;
        Debug.Log("SET");
    }

    private void Update()
    {
        MoveBody();
    }

    private void MoveBody()
    {
        titan.transform.position = Vector3.Lerp(titan.transform.position, bodyPosition, 5f * Time.deltaTime);
        //Debug.Log(bodyPosition);
    }

    public void SetActive(bool active)
    {
        this.active = active;
        if (active) GetNextLeg().MakeAStep();
    }

    public void StepIsOver (Leg leg)
    {
        if (active && leg == legs[currentLeg])
        {
            GetNextLeg().MakeAStep();
        }
    }

    private Leg GetNextLeg()
    {
        currentLeg += 1;
        if (currentLeg > legs.Count - 1)
            currentLeg = 0;
        Debug.Log(currentLeg);

        return legs[currentLeg];
    }
}