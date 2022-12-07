using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UrbanCalculator
{
    public void MeasureBuilding(Building building)
    {
        Building.minLength = Mathf.Min(building.Length, Building.minLength);
        Building.maxLength = Mathf.Max(Building.maxLength, building.Length);
        Building.averageLength = (Building.minLength + Building.maxLength) / 2;

        Building.minWidth = Mathf.Min(building.Width, Building.minWidth);
        Building.maxWidth = Mathf.Max(Building.maxWidth, building.Width);
        Building.averageWidth = (Building.minWidth + Building.maxWidth) / 2;

        Building.minArea = Building.minLength * Building.minWidth;
        Building.maxArea = Building.maxLength * Building.maxWidth;
        Building.averageArea = (Building.minArea + Building.maxArea) / 2;
    }
}
