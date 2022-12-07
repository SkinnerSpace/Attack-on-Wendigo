using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class OldConstructionArea
{
    public enum Type
    {
        Single,
        Head,
        Tail
    }

    public Type type = Type.Single;
    public OldConstructionArea headOfCluster;

    private const float EXPANSION_CHANCE = 0.5f;

    public static int areasKept;
    public static int areas;

    public int index { get; private set; }
    public Vector2 coords { get; private set; }

    private List<OldConstructionArea> availableAreas = new List<OldConstructionArea>();
    private List<OldConstructionArea> areasUnderConstruction = new List<OldConstructionArea>();
    private List<OldConstructionArea> tailAreas = new List<OldConstructionArea>();

    public OldConstructionArea(Vector2 coords)
    {
        this.coords = coords;
    }

    public void FoundNewStreet()
    {
        areasKept++;
        areas++;
        index = areas;
        Debug.Log(index);

        PlanOnTheSpot();
        CheckWorkCompletion();
    }

    public void ExpandOldStreet(OldConstructionArea headOfCluster)
    {
        areasKept++;

        index = headOfCluster.index;
        this.headOfCluster = headOfCluster;
        this.headOfCluster.AddToTail(this);

        PlanOnTheSpot();
        CheckWorkCompletion();
    }

    private void PlanOnTheSpot()
    {
        //availableAreas = Landowner.Instance.GetAvailableAreas(coords);

        if (availableAreas.Count > 0)
        {
            OccupyAvailableAreas();
            DefineAdjacentAreas();
        }
    }

    private void OccupyAvailableAreas()
    {
        for (int i = 0; i < availableAreas.Count; i++)
        {
            OldConstructionArea area = availableAreas[i];
            area.Occupy();
            areasUnderConstruction.Add(area);
        }
    }

    public void Occupy()
    {
        index = -1;
    }

    private void DefineAdjacentAreas()
    {
        foreach (OldConstructionArea area in availableAreas)
            BuildOrExpand(area);
    }

    private void BuildOrExpand(OldConstructionArea area)
    {
        switch (type)
        {
            case Type.Single:
                BuildOrExpandByChance(area);
                break;

            case Type.Head:
                BuildOrExpandByChance(area);
                break;

            case Type.Tail:
                area.FoundNewStreet();
                break;
        }

        if (type == Type.Single || type == Type.Head)
        {
            BuildOrExpandByChance(area);
        }
        else
        {
            area.FoundNewStreet();
        }
    }

    private void BuildOrExpandByChance(OldConstructionArea area)
    {
        float choice = UnityEngine.Random.Range(0, 1f);

        if (choice >= EXPANSION_CHANCE)
        {
            ExpandItselfToAdjacent(area);
        }
        else
        {
            area.FoundNewStreet();
        }
    }

    private void ExpandItselfToAdjacent(OldConstructionArea area)
    {
        HeadOfClusterIfSingle();

        area.ExpandOldStreet(headOfCluster);
        tailAreas.Add(area);
    }

    private void HeadOfClusterIfSingle()
    {
        if (type == Type.Single)
        {
            type = Type.Head;
            headOfCluster = this;
        }
    }

    public void AddToTail(OldConstructionArea area)
    {
        tailAreas.Add(area);
    }

    private void CheckWorkCompletion()
    {
        /*
        if (areasKept >= Landowner.Instance.areasCount)
            Landowner.Instance.AreasAreDefined();
        */
    }

    public void Sketch(float emptySpace, float occupiedSpace)
    {
        Vector2 emptyScale = new Vector2(emptySpace, emptySpace);
        Vector2 occupiedScale = new Vector2(occupiedSpace, occupiedSpace);
        Vector3 position = new Vector3((coords.x * emptyScale.x) + (emptyScale.x / 2), 0.5f, (coords.y * emptyScale.y) + (emptyScale.y / 2));

        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.localScale = new Vector3(occupiedScale.x, 1f, occupiedScale.y) * 0.1f;
        plane.transform.position = position;
    }
}
