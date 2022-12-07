using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Architect : MonoBehaviour
{
    private const int STREET_SIZE_ADJUSTMENT = 20;

    private Library library;
    private List<Building> availableBuildings = new List<Building>();
    private UrbanCalculator calculator;
    [SerializeField] private Transform blueprint;

    [SerializeField] private int minStreetSize = 2;
    [SerializeField] private int maxStreetSize = 4;

    public int buildingsCount { get; private set; }

    private void Awake()
    {
        library = GetComponentInChildren<Library>();
        calculator = new UrbanCalculator();

        //GoThroughTheList();
        //AllocateSpace();
    }

    private void GoThroughTheList()
    {
        for (int i=0; i < library.buildings.Count; i++)
        {
            Building building = library.buildings[i].GetComponent<Building>();
            calculator.MeasureBuilding(building);
        }

        //ShowBuildingMeasurements();
    }

    private void AllocateSpace()
    {
        float areaRatio = UnityEngine.Random.Range(0f, 1f);
        float inverseAreaRatio = 1f - areaRatio;

        float streetArea = UnityEngine.Random.Range(minStreetSize, maxStreetSize) * Building.averageArea;
        float streetLength = Mathf.Clamp(streetArea * areaRatio, Building.minLength, Building.maxLength);
        float streetWidth = Mathf.Clamp(streetArea * inverseAreaRatio, Building.minWidth, Building.maxWidth);

        Debug.Log("Ratio " + areaRatio);

        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.localScale = new Vector3(streetLength, 1f, streetWidth) / STREET_SIZE_ADJUSTMENT;
        plane.transform.position = blueprint.position + new Vector3(0f, 0.1f, 0f);
        plane.transform.SetParent(blueprint);

        Debug.Log("Street length " + streetLength);
        Debug.Log("Street width " + streetWidth);
    }

    private void ShowBuildingMeasurements()
    {
        Debug.Log("Min length " + Building.minLength);
        Debug.Log("Max length " + Building.maxLength);
        Debug.Log("Average length " + Building.averageLength);

        Debug.Log("Min width " + Building.minWidth);
        Debug.Log("Max width " + Building.maxWidth);
        Debug.Log("Average width" + Building.averageWidth);

        Debug.Log("Min area " + Building.minArea);
        Debug.Log("Max area " + Building.maxArea);
        Debug.Log("Average " + Building.averageArea);
    }
}
